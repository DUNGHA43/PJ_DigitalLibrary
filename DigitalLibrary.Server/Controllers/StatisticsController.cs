using DigitalLibrary.Server.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsServices _services;

        public StatisticsController(IStatisticsServices services)
        {
            _services = services;
        }

        [HttpGet("GetStatistics_noauthozire")]
        public async Task<IActionResult>GetStatistics()
        {
            var result = await _services.GetStatisticsAsync();
            return Ok(result);
        }
    }
}
