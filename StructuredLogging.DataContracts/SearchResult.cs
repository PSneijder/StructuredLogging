using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.DataContracts
{
    [DebuggerDisplay("{Results} {Groups}")]
    public struct SearchResult
    {
        public QueryFilterGroup[] Groups { get; }
        public SearchResultItem[] Results { get; }

        public SearchResult(IEnumerable<SearchResultItem> results, IEnumerable<QueryFilterGroup> groups)
            : this()
        {
            Results = results.ToArray();
            Groups = groups.ToArray();
        }
        
        public override string ToString()
        {
            return $"{Results} {Groups}";
        }
    }
}