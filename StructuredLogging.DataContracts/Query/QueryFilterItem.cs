
using System.Diagnostics;

namespace StructuredLogging.DataContracts.Query
{
    [DebuggerDisplay("{Name} {Value} {HitCount}")]
    public struct QueryFilterItem
    {
        public string Name { get; }
        public string Value { get; }
        public int HitCount { get; }

        public QueryFilterItem(string name, string value)
            : this(name, value, 0) { }

        public QueryFilterItem(string name, string value, int hitCount)
        {
            Name = name;
            Value = value;
            HitCount = hitCount;
        }
        
        public override string ToString()
        {
            return $"{Name} {Value} {HitCount}";
        }
    }
}