using System;
using BoboBrowse.Net;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StructuredLogging.Core.Contracts;
using StructuredLogging.Core.QueryParsers;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Extensions;
using LuceneQuery = Lucene.Net.Search.Query;
using QueryFilter = StructuredLogging.DataContracts.Query.Query;

namespace StructuredLogging.Core
{
    sealed class Searcher
        : ISearcher
    {
        private const bool ReadonlyMode = true;
        private readonly IFilterBuilder _builder;
        private readonly IFormater _formater;
        private readonly string _path;

        public Searcher(IFilterBuilder builder, IFormater formater, string path)
        {
            _builder = builder;
            _formater = formater;
            _path = path;
        }

        public SearchResult Search(SearchRequest searchRequest)
        {
            LuceneQuery query;
            if (string.IsNullOrWhiteSpace(searchRequest.Phrase))
            {
                query = new MatchAllDocsQuery();
            }
            else
            {
                var parser = new MultiFieldWildcardQueryParser(Constants.Version, Fields.CreatePerFields(), Fields.CreatePerFieldAnalyzers());
                query = parser.Parse(searchRequest.Phrase);
            }

            var browseRequest = new BrowseRequest
            {
                Count = searchRequest.Take > 0 ? searchRequest.Take : Constants.MaxNrOfSearchResults,
                Offset = searchRequest.Skip > 0 ? searchRequest.Skip : 0,
                FacetSpecs = Fields.CreatePerFieldFacetSpecs(),
                Sort = Fields.CreateSortFields(),
                Query = query
            };

            var filter = new QueryFilter
            {
                Properties = searchRequest.Properties,
                From = searchRequest.StartDate ?? DateTime.MinValue,
                To = searchRequest.EndDate ?? DateTime.MaxValue
            };

            browseRequest.AddQueryFilter(filter);

            return BrowseAsSearchResult(searchRequest, browseRequest);
        }

        public IEnumerable<RawEvent> SearchByQuery(QueryFilter query, int skip = 0, int take = Constants.MaxNrOfSearchResults)
        {
            var request = new BrowseRequest
            {
                Query = new MatchAllDocsQuery(),
                Count = take,
                Offset = skip,
                FacetSpecs = Fields.CreatePerFieldFacetSpecs()
            };

            request.AddQueryFilter(query);

            return BrowseAsRawEvent(request);
        }

        public IEnumerable<RawEvent> SearchByProperty(QueryFilterProperty property, int skip = 0, int take = Constants.MaxNrOfSearchResults)
        {
            var query = new MatchAllDocsQuery();

            var request = new BrowseRequest
            {
                Query = query,
                Count = take,
                Offset = skip,
                FacetSpecs = Fields.CreatePerFieldFacetSpecs()
            };

            request.AddQueryProperty(property);

            return BrowseAsRawEvent(request);
        }

        public IEnumerable<QueryFilterProperty> SearchProperties(int skip = 0, int take = Constants.MaxNrOfSearchResults)
        {
            var query = new MatchAllDocsQuery();

            var request = new BrowseRequest
            {
                Query = query,
                Count = take,
                Offset = skip,
                FetchStoredFields = true
            };

            request.AddSelection(new BrowseSelection(DataContracts.Constants.Facet.PropertyField) { SelectionOperation = BrowseSelection.ValueOperation.ValueOperationOr });
            request.SetFacetSpec(DataContracts.Constants.Facet.PropertyField, new FacetSpec { MinHitCount = 1, ExpandSelection = true });

            using (var indexReader = IndexReader.Open(FSDirectory.Open(_path), ReadonlyMode))
            {
                using (var boboIndexReader = BoboIndexReader.GetInstance(indexReader, Fields.CreatePerFieldFacetHandlers()))
                {
                    using (IBrowsable browser = new BoboBrowser(boboIndexReader))
                    {
                        using (BrowseResult result = browser.Browse(request))
                        {
                            IFacetAccessible propertyAccessible;
                            if (!result.FacetMap.TryGetValue(DataContracts.Constants.Facet.PropertyField, out propertyAccessible)) yield break;

                            IEnumerable<BrowseFacet> facetVals = propertyAccessible.GetFacets();
                            foreach (BrowseFacet facet in facetVals)
                            {
                                yield return new QueryFilterProperty(DataContracts.Constants.Facet.PropertyField, facet.Value, facet.FacetValueHitCount);
                            }
                        }
                    }
                }
            }
        }

        private SearchResult BrowseAsSearchResult(SearchRequest searchRequest, BrowseRequest browseRequest)
        {
            using (FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path)))
            { 
                using (IndexReader indexReader = IndexReader.Open(directory, ReadonlyMode))
                {
                    using (BoboIndexReader boboIndexReader = BoboIndexReader.GetInstance(indexReader, Fields.CreatePerFieldFacetHandlers()))
                    {
                        using (IBrowsable browser = new BoboBrowser(boboIndexReader))
                        {
                            using (BrowseResult result = browser.Browse(browseRequest))
                            {
                                var items = result.Hits
                                        .Select(hit => indexReader.Document(hit.DocId))
                                            .Select(doc => doc.ToSearchResultItem(_formater, result))
                                                .ToArray();

                                var groups = _builder.Create(result);

                                return new SearchResult(items, groups);
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<RawEvent> BrowseAsRawEvent(BrowseRequest request)
        {
            using (FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path)))
            {
                using (IndexReader indexReader = IndexReader.Open(directory, ReadonlyMode))
                {
                    using (BoboIndexReader boboIndexReader = BoboIndexReader.GetInstance(indexReader, Fields.CreatePerFieldFacetHandlers()))
                    {
                        using (IBrowsable browser = new BoboBrowser(boboIndexReader))
                        {
                            using (BrowseResult result = browser.Browse(request))
                            {
                                foreach (BrowseHit hit in result.Hits)
                                {
                                    var doc = indexReader.Document(hit.DocId);
                                    yield return doc.ToRawEvent();
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}