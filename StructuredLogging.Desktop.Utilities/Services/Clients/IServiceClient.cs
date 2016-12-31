using System.Collections.Generic;
using System.Threading.Tasks;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Query;

namespace StructuredLogging.Desktop.Utilities.Services.Clients
{
    public interface IServiceClient
    {
        Task<SearchResult> Search(SearchRequest request);
        Task<IEnumerable<QueryFilterItem>> GetFilterItems();
    }
}