using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.Services
{
    sealed class IndexerService
        : IIndexerService
    {
        private readonly IIndexer _indexer;

        public IndexerService(IIndexer indexer)
        {
            _indexer = indexer;
        }

        public void Index(RawEvents rawEvents)
        {
            _indexer.Index(rawEvents);
        }

        public void Index(RawEvent rawEvent)
        {
            _indexer.Index(rawEvent);
        }
    }
}