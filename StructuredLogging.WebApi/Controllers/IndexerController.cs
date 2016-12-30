using System;
using System.Web.Http;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.Services.Contracts;

namespace StructuredLogging.WebApi.Controllers
{
    [Route("index")]
    public class IndexerController
        : ApiController
    {
        private readonly IIndexerService _service;

        public IndexerController(IIndexerService service)
        {
            _service = service;
        }
        
        // POST api/index/test
        [HttpPost]
        public IHttpActionResult Post([FromBody] RawEvents rawEvents)
        {
            try
            {
                _service.Index(rawEvents);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}