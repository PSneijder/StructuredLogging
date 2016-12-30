using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StructuredLogging.DataContracts.Query
{
    [DebuggerDisplay("{Name} {Filters.Length}")]
    public class QueryFilterGroup
    {
        public string Name { get; }
        public QueryFilterProperty[] Filters { get; }

        public QueryFilterGroup(string name, IEnumerable<QueryFilterProperty> filters)
        {
            Name = name;
            Filters = filters.ToArray();
        }
    }
}