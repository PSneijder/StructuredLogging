using System.Collections.Generic;
using System.Linq;
using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.Extensions;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.Services
{
    sealed class IndexerService
        : IIndexerService
    {
        private readonly IIndexer _indexer;
        private readonly IFormater _formater;

        public IndexerService(IIndexer indexer, IFormater formater)
        {
            _indexer = indexer;
            _formater = formater;
        }

        public void Index(RawEvents rawEvents)
        {
            _indexer.Index(rawEvents);
        }

        public void Index(RawEvent rawEvent)
        {
            _indexer.Index(rawEvent);
        }

        public SearchResultItem ToSearchResult(RawEvent rawEvent)
        {
            return rawEvent.ToSearchResultItem(_formater);
        }

        public IEnumerable<SearchResultItem> ToSearchResults(RawEvents rawEvents)
        {
            return rawEvents.Events.Select(rawEvent => rawEvent.ToSearchResultItem(_formater));
        }
    }
}