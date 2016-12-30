using System.Collections.Generic;
using System.Collections.ObjectModel;
using BoboBrowse.Net;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Core
{
    sealed class FilterBuilder
        : IFilterBuilder
    {
        private readonly Dictionary<string, string> _translations = new Dictionary<string, string>
        {
            { DataContracts.Constants.Facet.Level, "Level" }
        };

        public IEnumerable<QueryFilterGroup> Create(BrowseResult result)
        {
            foreach (KeyValuePair<string, IFacetAccessible> accessible in result.FacetMap)
            {
                IList<QueryFilterProperty> filters = new Collection<QueryFilterProperty>();
                IEnumerable<BrowseFacet> facetVals = accessible.Value.GetFacets();

                foreach (BrowseFacet facet in facetVals)
                {
                    var property = new QueryFilterProperty(accessible.Key, facet.Value, facet.FacetValueHitCount);
                    filters.Add(property);
                }

                string key = accessible.Key;

                if (_translations.ContainsKey(accessible.Key))
                    key = _translations[accessible.Key];

                yield return new QueryFilterGroup(key, filters);
            }
        }
    }
}