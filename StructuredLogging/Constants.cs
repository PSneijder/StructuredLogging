using BoboBrowse.Net;
using BoboBrowse.Net.Facets;
using BoboBrowse.Net.Facets.Impl;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;
using System.Collections.Generic;
using Lucene.Net.Search;
using StructuredLogging.Core.Analyzers;
using static StructuredLogging.DataContracts.Constants;

namespace StructuredLogging
{
    static class Fields
    {
        public static string Timestamp = "Field_Timestamp";
        public static string MessageTemplate = "Field_MessageTemplate";
        public static string Properties = "Field_Properties";

        public static IDictionary<string, FacetSpec> CreatePerFieldFacetSpecs()
        {
            return new Dictionary<string, FacetSpec>
            {
                { Facet.Level, new FacetSpec { MinHitCount = 1, MaxCount = 25, OrderBy = FacetSpec.FacetSortSpec.OrderHitsDesc} },
                //{ Facet.PropertyField, new FacetSpec { MinHitCount = 1, MaxCount = 25, OrderBy = FacetSpec.FacetSortSpec.OrderHitsDesc } }
            };
        }

        public static IFacetHandler[] CreatePerFieldFacetHandlers()
        {
            return new IFacetHandler[]
            {
                new SimpleFacetHandler(Facet.Level),
                //new MultiValueFacetHandler(Facet.PropertyField)
            };
        }

        public static Analyzer CreatePerFieldAnalyzers()
        {
            return new PerFieldAnalyzerWrapper(new StandardAnalyzer(Constants.Version),
                new List<KeyValuePair<string, Analyzer>>
                {
                    new KeyValuePair<string, Analyzer>(MessageTemplate, new CaseInsensitiveWhitespaceAnalyzer()),
                    new KeyValuePair<string, Analyzer>(Properties, new CaseInsensitiveWhitespaceAnalyzer())
                });
        }

        public static string[] CreatePerFields()
        {
            return new[]
            {
                MessageTemplate, Properties
            };
        }

        public static SortField[] CreateSortFields()
        {
            return new[]
            {
                new SortField(Timestamp, SortField.DOUBLE, true)
            };
        }
    }

    public static class Constants
    {
        public static Version Version = Version.LUCENE_30;

        public const int MaxNrOfSearchResults = 50;
    }
}