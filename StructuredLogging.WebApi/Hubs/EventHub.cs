using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using StructuredLogging.DataContracts;

namespace StructuredLogging.WebApi.Hubs
{
    public class EventHub
        : Hub
    {
        public void BroadcastEvents(IEnumerable<SearchResultItem> items)
        {
            Clients.All.BroadcastEvents(items);
        }

        public void BroadcastEvent(SearchResultItem item)
        {
            Clients.All.BroadcastEvent(item);
        }
    }
}