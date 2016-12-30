using StructuredLogging.DataContracts;

namespace StructuredLogging.Services.Contracts
{
    public interface ISearcherService
    {
        SearchResult Search(string phrase);
        SearchResult Search(SearchRequest request);
    }
}