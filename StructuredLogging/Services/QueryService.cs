using System;
using System.Collections.Generic;
using System.Linq;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.Services
{
    sealed class QueryService
        : IQueryService
    {
        private readonly ISearcher _searcher;
        private readonly IFormater _formater;

        public QueryService(ISearcher searcher, IFormater formater)
        {
            _searcher = searcher;
            _formater = formater;
        }

        public IEnumerable<QueryFilterProperty> GetQueryProperties()
        {
            var queries = _searcher.SearchProperties()
                    .ToList();

            return queries;
        }

        public SearchResult Query()
        {
            var result = _searcher.Search(new SearchRequest(string.Empty));

            return result;
        }

        public IEnumerable<SearchResultItem> QueryBy(Query query)
        {
            var rawEvents = _searcher.SearchByQuery(query)
                    .ToList();

            foreach (var rawEvent in rawEvents)
            {
                var message = _formater.FormatEvent(rawEvent);
                var timeStamp = DateTime.Parse(rawEvent.Timestamp);

                yield return new SearchResultItem(rawEvent.Level, timeStamp, message);
            }
        }

        public IEnumerable<SearchResultItem> QueryBy(QueryFilterProperty property)
        {
            var rawEvents = _searcher.SearchByProperty(property)
                    .ToList();

            foreach (var rawEvent in rawEvents)
            {
                var message = _formater.FormatEvent(rawEvent);
                var timeStamp = DateTime.Parse(rawEvent.Timestamp);

                yield return new SearchResultItem(rawEvent.Level, timeStamp, message);
            }
        }
    }
}