using CommonShared;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.TruckHandlers.Commands;

namespace PraktikaApi.Controllers
{
    public class TruckController : ControllerBaseApi
    {
        [HttpPost]
        public IActionResult AddTruck([FromBody] TruckAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpGet]
        public IActionResult GetTruckList([FromQuery] TruckGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
