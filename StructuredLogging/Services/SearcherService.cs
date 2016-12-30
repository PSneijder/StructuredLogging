using StructuredLogging.Core.Contracts;
using StructuredLogging.DataContracts;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.Services
{
    sealed class SearcherService
        : ISearcherService
    {
        private readonly ISearcher _searcher;
        private readonly IFormater _formater;

        public SearcherService(ISearcher searcher, IFormater formater)
        {
            _searcher = searcher;
            _formater = formater;
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