using CommonShared;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.RouteHandlers.RouteCommand;
using PraktikaApplication.TrailerHandlers.Commands;

namespace PraktikaApi.Controllers
{
    public class TrailerController : ControllerBaseApi
    {
        [HttpPost]
        public IActionResult AddTrailer([FromBody] TrailerAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [HttpGet]
        public IActionResult GetTrailerList([FromQuery] TrailerGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
