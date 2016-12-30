using System;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Core.Contracts
{
    interface IIndexer
        : IDisposable
    {
        void Index(RawEvents rawEvents);

        void Index(params RawEvent[] rawEvents);
    }
}