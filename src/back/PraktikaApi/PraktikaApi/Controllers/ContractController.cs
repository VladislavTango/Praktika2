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
        public IActionResult GetContracts([FromQuery] ContractGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateContracts([FromBody] ContractUpdateCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteContract([FromBody] ContractDeleteCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddContract([FromBody] ContractAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
