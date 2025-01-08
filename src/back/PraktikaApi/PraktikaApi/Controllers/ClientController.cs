using CommonShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApi.Controllers
{
    public class ClientController : ControllerBaseApi
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetClients([FromQuery]ClientGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpPut]
        public IActionResult UpdateClients([FromBody]ClientUpdateCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpDelete]
        public IActionResult DeleteClient([FromBody]ClientDeleteCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpPost("add")]
        public IActionResult AddClients([FromBody] ClientAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpPost("enter")]
        public IActionResult EnterClient([FromBody] ClientEnterCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpPost("confirm code")]
        public IActionResult SendCode([FromBody] ClientSendCodeCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
