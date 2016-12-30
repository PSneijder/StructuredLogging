using System.Diagnostics;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Desktop.Utilities.Models.Filter
{
    [DebuggerDisplay("{IsChecked} {Item.Name} {Item.Value}")]
    public class FilterItem
    {
        public bool IsChecked { get; set; }
        public QueryFilterProperty Item { get; }

        public FilterItem(QueryFilterProperty property)
            : this(property, false) { }

        public FilterItem(QueryFilterProperty property, bool isChecked)
        {
            Item = property;
            IsChecked = isChecked;
        }

        public static implicit operator FilterItem(QueryFilterProperty property)
        {
            return new FilterItem(property);
        }
    }
}