using System.Collections.Generic;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Services.Contracts
{
    public interface IQueryService
    {
        IEnumerable<QueryFilterProperty> GetQueryProperties();

        SearchResult Query();

        IEnumerable<SearchResultItem> QueryBy(Query query);

        IEnumerable<SearchResultItem> QueryBy(QueryFilterProperty property);
    }
}