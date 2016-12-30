using System;
using System.Diagnostics;

namespace StructuredLogging.DataContracts.Query
{
    [DebuggerDisplay("{Properties} {From} {To}")]
    public struct Query
    {
        public QueryFilterProperty[] Properties { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}