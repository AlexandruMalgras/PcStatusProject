using Microsoft.AspNetCore.Mvc;

namespace PcStatusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly CpuStatusData cpuStatusData;

        public StatusController(CpuStatusData statusData)
        {
            this.cpuStatusData = statusData;
        }

        [HttpGet("cpu-temperature")]
        public async Task<IActionResult> GetCpuTemperature()
        {
            await cpuStatusData.UpdateCpuTemperatureAsync();
            return Ok(cpuStatusData.CpuTemperature);
        }

        [HttpGet("cpu-load")]
        public async Task<IActionResult> GetCpuLoad()
        {
            await cpuStatusData.UpdateCpuLoadAsync();
            return Ok(cpuStatusData.CpuLoad);
        }

        [HttpGet("cpu-speed")]
        public async Task<IActionResult> GetCpuSpeed()
        {
            await cpuStatusData.UpdateCpuSpeedAsync();
            return Ok(cpuStatusData.CpuSpeed);
        }
    }
}
