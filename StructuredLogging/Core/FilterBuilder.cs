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
                IList<QueryFilterItem> filters = new Collection<QueryFilterItem>();
                IEnumerable<BrowseFacet> facetVals = accessible.Value.GetFacets();

                foreach (BrowseFacet facet in facetVals)
                {
                    var property = new QueryFilterItem(accessible.Key, facet.Value, facet.FacetValueHitCount);
                    filters.Add(property);
                }

                string name = accessible.Key;

                if (_translations.ContainsKey(accessible.Key))
                { 
                    name = _translations[accessible.Key];
                }

                yield return new QueryFilterGroup(name, filters);
            }
        }
    }
}