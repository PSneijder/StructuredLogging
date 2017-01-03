using System;
using System.Diagnostics;

namespace StructuredLogging.DataContracts
{
    [DebuggerDisplay("{Timestamp} {Level} {Message}")]
    public class SearchResultItem
        : IComparable
    {
        public string Level { get; private set; }
        public string Message { get; private set; }
        public DateTime Timestamp { get; private set; }
        
        public SearchResultItem(string level, DateTime timestamp, string message)
        {
            Level = level;
            Timestamp = timestamp;
            Message = message;
        }

        public override string ToString()
        {
            return $"{Timestamp:s} {Level} {Message}";
        }

        public int CompareTo(object o)
        {
            SearchResultItem other = (SearchResultItem) o;

            return DateTime.Compare(other.Timestamp, Timestamp);
        }
    }
}