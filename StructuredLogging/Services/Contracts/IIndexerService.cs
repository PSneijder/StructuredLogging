using System.Collections.Generic;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;

namespace StructuredLogging.Services.Contracts
{
    public interface IIndexerService
    {
        void Index(RawEvents rawEvents);
        void Index(RawEvent rawEvent);

        SearchResultItem ToSearchResult(RawEvent rawEvent);
        IEnumerable<SearchResultItem> ToSearchResults(RawEvents rawEvents);
    }
}