using System;
using System.Web.Http;
using StructuredLogging.DataContracts;
using Swashbuckle.Swagger.Annotations;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.WebApi.Controllers
{
    [RoutePrefix(Constants.RoutePrefix)]
    public class SearcherController
        : ApiController
    {
        private readonly ISearcherService _service;

        public SearcherController(ISearcherService service)
        {
            _service = service;
        }
        
        // GET api/search/test
        [SwaggerOperation("GetByRequest")]
        [HttpPost]
        [Route(Constants.RouteSearch)]
        public IHttpActionResult Get([FromBody] SearchRequest request)
        {
            try
            {
                var results = _service.Search(request);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/search/test
        [SwaggerOperation("GetByPhrase")]
        [HttpGet]
        [Route(Constants.RouteSearch + "/{phrase}")]
        public IHttpActionResult Get(string phrase)
        {
            try
            {
                var results = _service.Search(new SearchRequest(phrase));

                return Ok(results);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}