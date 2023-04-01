using Microsoft.AspNetCore.Mvc;
using PcStatusAPI.Components;

namespace PcStatusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureKeyDataController : ControllerBase
    {
        private AzureStatus azureStatus;

        public AzureKeyDataController()
        {
            azureStatus = AzureStatus.Instance;
        }

        [HttpGet("max-cpu-temperature")]
        public async Task<IActionResult> GetMaxCpuTemperature()
        {
            return Ok(new { maxCpuTemperature = azureStatus.MaxCpuTemperature });
        }

        [HttpGet("max-cpu-load")]
        public async Task<IActionResult> GetMaxCpuLoad()
        {
            return Ok(new { maxCpuLoad = azureStatus.MaxCpuLoad });
        }

        [HttpGet("max-cpu-speed")]
        public async Task<IActionResult> GetMaxCpuSpeed()
        {
            return Ok(new { maxCpuSpeed = azureStatus.MaxCpuSpeed });
        }
    }
}
