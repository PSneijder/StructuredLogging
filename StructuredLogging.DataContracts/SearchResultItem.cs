using System;
using System.Diagnostics;

namespace StructuredLogging.DataContracts
{
    [DebuggerDisplay("{Timestamp} {Level} {Message}")]
    public struct SearchResultItem
    {
        public string Level { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }
        
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