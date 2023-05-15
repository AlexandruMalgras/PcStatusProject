using Microsoft.AspNetCore.Mvc;
using PcStatusAPI.Components;

namespace PcStatusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly CpuStatus cpuStatus;

        public StatusController()
        {
            this.cpuStatus = CpuStatus.Instance;
        }

        [HttpGet("cpu-name")]
        public IActionResult GetCpuName()
        {
            return Ok(new { name = cpuStatus.CpuName });
        }

        [HttpGet("cpu-temperature")]
        public IActionResult GetCpuTemperature()
        {
            return Ok(new { temperature = cpuStatus.CpuTemperature });
        }

        [HttpGet("cpu-load")]
        public IActionResult GetCpuLoad()
        {
            return Ok(new { load = cpuStatus.CpuLoad });
        }

        [HttpGet("cpu-speed")]
        public IActionResult GetCpuSpeed()
        {
            return Ok(new { speed = cpuStatus.CpuSpeed });
        }
    }
}
