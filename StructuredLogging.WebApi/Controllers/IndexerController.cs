using System;
using System.Web.Http;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.Services.Contracts;
using Swashbuckle.Swagger.Annotations;

namespace StructuredLogging.WebApi.Controllers
{
    [RoutePrefix(Constants.RoutePrefix)]
    public class IndexerController
        : ApiController
    {
        private readonly IIndexerService _service;

        public IndexerController(IIndexerService service)
        {
            _service = service;
        }

        // POST api/index
        [SwaggerOperation("CreateRawEvents")]
        [HttpPost]
        [Route(Constants.RouteEvents)]
        public IHttpActionResult CreateRawEvents([FromBody] RawEvents rawEvents)
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

        // POST api/index
        [SwaggerOperation("CreateRawEvent")]
        [HttpPost]
        [Route(Constants.RouteEvent)]
        public IHttpActionResult CreateRawEvent([FromBody] RawEvent rawEvent)
        {
            try
            {
                _service.Index(rawEvent);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}