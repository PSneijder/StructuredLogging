using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Services.Contracts
{
    public interface IIndexerService
    {
        void Index(RawEvents rawEvents);
        void Index(RawEvent rawEvent);
    }
}