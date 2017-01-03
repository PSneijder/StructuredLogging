using System;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using StructuredLogging.DataContracts.Event;
using StructuredLogging.Services.Contracts;
using StructuredLogging.WebApi.Hubs;
using Swashbuckle.Swagger.Annotations;

namespace StructuredLogging.WebApi.Controllers
{
    [RoutePrefix(Constants.RoutePrefix)]
    public class IndexerController
        : ApiController
    {
        private readonly IIndexerService _service;
        private readonly IHubContext _eventHub;

        public IndexerController(IIndexerService service)
        {
            _service = service;

            _eventHub = GlobalHost.ConnectionManager.GetHubContext<EventHub>();
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

                _eventHub.Clients.All.BroadcastEvents(rawEvents);

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

                _eventHub.Clients.All.BroadcastEvent(rawEvent);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}