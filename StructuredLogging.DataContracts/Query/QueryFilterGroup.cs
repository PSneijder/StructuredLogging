using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StructuredLogging.DataContracts.Query
{
    [DebuggerDisplay("{Name} {Filters.Length}")]
    public class QueryFilterGroup
    {
        public string Name { get; }
        public QueryFilterItem[] Filters { get; }

        public QueryFilterGroup(string name, IEnumerable<QueryFilterItem> filters)
        {
            Name = name;
            Filters = filters.ToArray();
        }

        public override string ToString()
        {
            return $"{Name} {Filters.Length}";
        }
    }
}