using CommonShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.OrderHandlers.OrderCommand;

namespace PraktikaApi.Controllers
{
    public class OrderController : ControllerBaseApi
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetOrders([FromQuery] OrderGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateOrders([FromBody] OrderUpdateCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteOrders([FromBody] OrderDeleteCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddOrders([FromBody] OrderAddCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}

