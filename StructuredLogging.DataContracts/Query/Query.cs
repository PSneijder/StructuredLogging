using System;
using System.Diagnostics;

namespace StructuredLogging.DataContracts.Query
{
    [DebuggerDisplay("{Items} {From} {To}")]
    public struct Query
    {
        public QueryFilterItem[] Items { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public override string ToString()
        {
            return $"{From} {To} {Items}";
        }
    }
}