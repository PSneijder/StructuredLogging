using System.Collections.Generic;
using StructuredLogging.DataContracts;

namespace StructuredLogging.Desktop.Utilities.Services.Clients
{
    public delegate void EventReceivedEventHandler(SearchResultItem item);
    public delegate void EventsReceivedEventHandler(IEnumerable<SearchResultItem> items);
    
    public interface IHubClient
    {
        void Connect();
        void Disconnect();

        event EventReceivedEventHandler EventReceived;
        event EventsReceivedEventHandler EventsReceived;
    }
}