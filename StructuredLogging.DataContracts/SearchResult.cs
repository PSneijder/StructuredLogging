using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.DataContracts
{
    [DebuggerDisplay("{Items} {Groups}")]
    public class SearchResult
    {
        public QueryFilterGroup[] Groups { get; private set; }
        public SearchResultItem[] Items { get; private set; }

        public SearchResult(IEnumerable<SearchResultItem> items, IEnumerable<QueryFilterGroup> groups)
        {
            Items = items.ToArray();
            Groups = groups.ToArray();
        }
        
        public override string ToString()
        {
            return $"{Items} {Groups}";
        }
    }
}