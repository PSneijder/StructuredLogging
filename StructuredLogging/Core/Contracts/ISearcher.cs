using System;
using System.Collections.Generic;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Core.Contracts
{
    interface ISearcher
        : IDisposable
    {
        SearchResult Search(SearchRequest request);
        
        IEnumerable<RawEvent> SearchByQuery(Query query, int skip = 0, int take = Constants.MaxNrOfSearchResults);

        IEnumerable<RawEvent> SearchByProperty(QueryFilterItem item, int skip = 0, int take = Constants.MaxNrOfSearchResults);

        IEnumerable<QueryFilterItem> SearchProperties(int skip = 0, int take = Constants.MaxNrOfSearchResults);
    }
}