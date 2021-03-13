using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDB
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMyRep _repository;

        public ValuesController(IMyRep repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(_repository.GetAllEmployees());
        }
    }
}
