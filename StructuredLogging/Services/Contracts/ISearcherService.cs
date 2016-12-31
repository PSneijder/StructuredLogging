using StructuredLogging.DataContracts;

namespace StructuredLogging.Services.Contracts
{
    public interface ISearcherService
    {
        SearchResult Search(SearchRequest request);
    }
}