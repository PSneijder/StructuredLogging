using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.Desktop.Utilities.Services.Clients
{
    sealed class RestHttpClient
        : HttpClient
    {
        public RestHttpClient(string baseUrl)
        {
            BaseAddress = new Uri(baseUrl);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.HeaderMediaTypeJson));
        }
    }

    public sealed class ServiceClient
        : ConfigurationReaderBase
            , IServiceClient
    {
        private readonly ISearcherService _searcherService;
        private readonly IQueryService _queryService;
        private readonly string _baseUrl;
        private readonly string _site;

        public ServiceClient(Configuration configuration, ISearcherService searcherService, IQueryService queryService)
            : base(configuration)
        {
            _searcherService = searcherService;
            _queryService = queryService;

            string url = ReadSetting("baseUrl");
            Uri uri = new Uri(url);

            _baseUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}";
            _site = url;

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }
        
        public async Task<SearchResult> Search(SearchRequest request)
        {
            return await Task.Factory.StartNew(() => _searcherService.Search(request));
        }

        public async Task<IEnumerable<QueryFilterProperty>> GetQueryFilterProperties()
        {
            return await Task.Factory.StartNew(() => _queryService.GetQueryProperties());
        }
    }
}