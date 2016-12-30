using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Desktop.Utilities.Models.Filter
{
    [DebuggerDisplay("{Name} {Filters.Length}")]
    public class FilterGroup
    {
        public string Name { get; }
        public FilterItem[] Filters { get; }

        public FilterGroup(string name, IEnumerable<FilterItem> filters)
        {
            Name = name;
            Filters = filters.ToArray();
        }

        public static implicit operator FilterGroup(QueryFilterGroup group)
        {
            var filters = group.Filters
                .Select(filter => new FilterItem(filter));

            return new FilterGroup(group.Name, filters);
        }
    }
}