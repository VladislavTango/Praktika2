using CommonShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.InvoiceHandlers.Commands;
using PraktikaApplication.InvoicesHandlers.Commands;

namespace PraktikaApi.Controllers
{
    public class InvoiceController : ControllerBaseApi
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetInvoices([FromQuery] InvoicesGetListCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpGet("info")]
        public IActionResult GetInfo([FromQuery] InvoiceGetInfoCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateClients([FromBody] InvoicesUpdateStatusCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}
