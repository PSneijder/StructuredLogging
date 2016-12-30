using System.Collections.Generic;

namespace StructuredLogging.DataContracts.Event
{
    public sealed class RawEvents
    {
        public RawEvent[] Events { get; set; }
    }

    public sealed class RawEvent
    {
        public string Timestamp { get; set; }

        public string Level { get; set; }

        public string MessageTemplate { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public string Exception { get; set; }
    }
}