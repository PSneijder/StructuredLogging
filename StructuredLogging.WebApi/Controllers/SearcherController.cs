using System;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.WebApi.Controllers
{
    [Route("search")]
    public class SearcherController
        : ApiController
    {
        private readonly ISearcherService _service;

        public SearcherController(ISearcherService service)
        {
            _service = service;
        }
        
        // GET api/search/test
        [SwaggerOperation("GetByPhrase")]
        [HttpGet]
        public IHttpActionResult Get(string phrase)
        {
            try
            {
                var results = _service.Search(phrase);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}