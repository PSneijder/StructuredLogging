using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.Services
{
    sealed class SearcherService
        : ISearcherService
    {
        private readonly ISearcher _searcher;

        public SearcherService(ISearcher searcher)
        {
            _searcher = searcher;
        }

        public SearchResult Search(string phrase)
        {
            var result = _searcher.Search(new SearchRequest(phrase));
            return result;
        }

        public SearchResult Search(SearchRequest request)
        {
            var result = _searcher.Search(request);
            return result;
        }
    }
}