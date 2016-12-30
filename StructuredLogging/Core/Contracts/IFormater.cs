using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Core.Contracts
{
    interface IFormater
    {
        string FormatEvent(RawEvent logEvent);
    }
}