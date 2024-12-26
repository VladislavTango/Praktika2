using CommonShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.TransportationHandlers.TransportationCommand;

namespace PraktikaApi.Controllers
{
    public class TransportationController : ControllerBaseApi
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetClients([FromQuery] TransportationGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateClients([FromBody] TransportationUpdateCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteClient([FromBody] TransportationDeleteCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddCliebts([FromBody] TransportationAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }

    }
}
