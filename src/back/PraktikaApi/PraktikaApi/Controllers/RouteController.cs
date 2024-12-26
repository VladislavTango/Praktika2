﻿using CommonShared;
using Microsoft.AspNetCore.Mvc;
using PraktikaApplication.RouteHandlers.RouteCommand;

namespace PraktikaApi.Controllers
{
    public class RouteController : ControllerBaseApi
    {
        [HttpGet]
        public IActionResult AddCliebts([FromQuery] CalculateRouteCommand command)
        {
            var responce = Mediator.Send(command);
            return Ok(responce);
        }
    }
}