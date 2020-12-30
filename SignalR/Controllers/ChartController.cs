﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalR.DataStorage;
using SignalR.Hubs;
using SignalR.TimerFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SignalR.Controllers
{

        [Route("api/ChartCon")]
        [ApiController]
        public class ChartController : ControllerBase
        {
            private IHubContext<ChartHub> _hub;
            public ChartController(IHubContext<ChartHub> hub)
            {
                _hub = hub;
            }
            [HttpGet("Chart")]
            public IActionResult Get()
            {
                var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
                return Ok(new { Message = "Request Completed" });
            }
        }
}
