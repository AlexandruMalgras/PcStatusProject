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

        [HttpGet("cpu-name")]
        public async Task<IActionResult> GetCpuName()
        {
            await cpuStatusData.UpdateCpuNameAsync();
            return Ok(new { name = cpuStatusData.CpuName });
        }

        [HttpGet("cpu-temperature")]
        public async Task<IActionResult> GetCpuTemperature()
        {
            await cpuStatusData.UpdateCpuTemperatureAsync();
            return Ok(new { temperature = cpuStatusData.CpuTemperature });
        }

        [HttpGet("cpu-load")]
        public async Task<IActionResult> GetCpuLoad()
        {
            await cpuStatusData.UpdateCpuLoadAsync();

            while (cpuStatusData.CpuLoad == 0)
            {
                await cpuStatusData.UpdateCpuLoadAsync();
            }

            return Ok(new { load = cpuStatusData.CpuLoad });
        }

        [HttpGet("cpu-speed")]
        public async Task<IActionResult> GetCpuSpeed()
        {
            await cpuStatusData.UpdateCpuSpeedAsync();
            return Ok(new { speed = cpuStatusData.CpuSpeed });
        }
    }
}
