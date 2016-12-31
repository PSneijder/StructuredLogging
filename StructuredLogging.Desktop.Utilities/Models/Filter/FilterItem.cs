using System.Diagnostics;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Desktop.Utilities.Models.Filter
{
    [DebuggerDisplay("{IsChecked} {Item.Name} {Item.Value}")]
    public class FilterItem
    {
        public bool IsChecked { get; set; }
        public QueryFilterItem Item { get; }

        public FilterItem(QueryFilterItem item)
            : this(item, false) { }

        public FilterItem(QueryFilterItem item, bool isChecked)
        {
            Item = item;
            IsChecked = isChecked;
        }

        public static implicit operator FilterItem(QueryFilterItem item)
        {
            return new FilterItem(item);
        }
    }
}