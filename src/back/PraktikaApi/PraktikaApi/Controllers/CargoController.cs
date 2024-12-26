using CommonShared;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.CargoHandlers.CargoCommand;

namespace PraktikaApi.Controllers
{
    public class CargoController : ControllerBaseApi
    {
        [HttpGet]
        public IActionResult GetClients([FromQuery] CargoGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpPut]
        public IActionResult UpdateClients([FromBody] CargoUpdateCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpDelete]
        public IActionResult DeleteClient([FromBody] CargoDeleteCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpPost]
        public IActionResult AddCliebts([FromBody] CargoAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
