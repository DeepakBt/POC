using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.SignalR;

namespace CoreSignalR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private IHubContext<ChartHub> _hub;
        public ChartController(IHubContext<ChartHub> hub)
        {
            _hub = hub;
        }
        public IActionResult GetData()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
            return Ok(new { Message = "Request Completed" });
        }
    }
}
