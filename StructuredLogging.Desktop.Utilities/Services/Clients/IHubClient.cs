using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Desktop.Utilities.Services.Clients
{
    public delegate void EventReceivedEventHandler(RawEvent rawEvent);
    public delegate void EventsReceivedEventHandler(RawEvents rawEvents);
    
    public interface IHubClient
    {
        void Connect();
        void Disconnect();

        event EventReceivedEventHandler EventReceived;
        event EventsReceivedEventHandler EventsReceived;
    }
}