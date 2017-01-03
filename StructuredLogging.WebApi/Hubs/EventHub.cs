using Microsoft.AspNet.SignalR;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.WebApi.Hubs
{
    public class EventHub
        : Hub
    {
        public void BroadcastEvents(RawEvents rawEvents)
        {
            Clients.All.BroadcastEvents(rawEvents);
        }

        public void BroadcastEvent(RawEvent rawEvent)
        {
            Clients.All.BroadcastEvent(rawEvent);
        }
    }
}