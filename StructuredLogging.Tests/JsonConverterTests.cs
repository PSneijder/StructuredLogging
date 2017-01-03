using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using StructuredLogging.DataContracts;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Desktop.Utilities.Services.Resolvers;

namespace StructuredLogging.Tests
{
    [TestClass]
    public class JsonConverterTests
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateContractResolver()
        };

        [TestMethod]
        public void TestSearchResultConverter()
        {
            // Given
            var searchResult = new SearchResult(new [] { new SearchResultItem("Verbose", DateTime.Now, "Test") }, Enumerable.Empty<QueryFilterGroup>());
            var jsonString = JsonConvert.SerializeObject(searchResult);

            // When
            var result = JsonConvert.DeserializeObject<SearchResult>(jsonString, _settings);

            // Then
            Assert.IsNotNull(result);
        }
    }
}