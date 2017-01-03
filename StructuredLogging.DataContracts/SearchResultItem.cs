using System;
using System.Diagnostics;

namespace StructuredLogging.DataContracts
{
    [DebuggerDisplay("{Timestamp} {Level} {Message}")]
    public struct SearchResultItem
    {
        public string Level { get; private set; }
        public string Message { get; private set; }
        public DateTime Timestamp { get; private set; }
        
        public SearchResultItem(string level, DateTime timestamp, string message)
            : this()
        {
            Level = level;
            Timestamp = timestamp;
            Message = message;
        }

        public override string ToString()
        {
            return $"{Timestamp:s} {Level} {Message}";
        }
    }
}