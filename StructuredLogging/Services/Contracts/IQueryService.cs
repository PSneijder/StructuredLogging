using System.Collections.Generic;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Services.Contracts
{
    public interface IQueryService
    {
        SearchResult Query();

        IEnumerable<SearchResultItem> Query(Query query);

        IEnumerable<QueryFilterItem> GetFilterItems();
    }
}