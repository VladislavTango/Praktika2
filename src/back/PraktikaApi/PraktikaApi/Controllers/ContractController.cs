using CommonShared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.ContractHandlers.ContractHandler;

namespace PraktikaApi.Controllers
{
    public class ContractController  : ControllerBaseApi
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetClients([FromQuery] ContractGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateClients([FromBody] ContractUpdateCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteClient([FromBody] ContractDeleteCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddCliebts([FromBody] ContractAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
