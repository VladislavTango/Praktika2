using CommonShared;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.VehicleHandlers.Commands;

namespace PraktikaApi.Controllers
{
    public class VehicleController : ControllerBaseApi
    {
        [HttpPost]
        public IActionResult AddVehicle([FromBody] VehicleAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpGet]
        public IActionResult GetListVehicle([FromQuery] VehicleGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpGet("by_cargo")]
        public IActionResult GetListVehicleByCargo([FromQuery] GetVehiclesByCargoCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpPut]
        public IActionResult RedactVehicles([FromBody] VehicleRedactCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
