
using System.Diagnostics;

namespace StructuredLogging.DataContracts.Query
{
    [DebuggerDisplay("{Name} {Value} {HitCount}")]
    public struct QueryFilterItem
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public int HitCount { get; private set; }

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