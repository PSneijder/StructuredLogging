using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Desktop.Utilities.Services.Resolvers;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.Desktop.Utilities.Services.Clients
{
    sealed class WebApiClient
        : HttpClient
    {
        public WebApiClient(string baseUrl)
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
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateContractResolver()
        };

        private readonly IQueryService _queryService;
        private readonly string _baseUrl;

        public ServiceClient(Configuration configuration, IQueryService queryService)
            : base(configuration)
        {
            _queryService = queryService; // ToDo: Change to WebApi

            _baseUrl = ReadSetting("baseUrl");

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }
        
        public async Task<SearchResult> Search(SearchRequest request)
        {
            // url: /api/event/
            string url = $"{_baseUrl}/{Constants.RoutePrefix}/{Constants.RouteSearch}";

            string jsonString = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, Constants.HeaderMediaTypeJson);

            using (WebApiClient client = new WebApiClient(_baseUrl))
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    SearchResult result = JsonConvert.DeserializeObject<SearchResult>(jsonString, _settings);
                    return result;
                }

                string messageString = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(messageString);
            }
        }

        public async Task<IEnumerable<QueryFilterItem>> GetFilterItems()
        {
            return await Task.Factory.StartNew(() => _queryService.GetFilterItems());
        }
    }
}