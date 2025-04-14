using DigitalLibrary.Server.Services.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("getviewanddowloadstatistic")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetViewAndDowloadStatisticAsync()
        {
            var result = await _services.GetViewAndDowloadStatisticAsync();

            if (result == null)
            {
                return NotFound("No data statistics!");
            }

            return Ok(result);
        }

        [HttpGet("getstats")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var result = await _services.GetStatsAsync("day");

            if (result == null)
            {
                return NotFound("No data statistics!");
            }

            return Ok(result);
        }

        [HttpPut("updateviewanddowloaded")]
        [Authorize]
        public async Task<IActionResult> UpdateViewAndDowloaded([FromQuery] int documentId, string? update = null)
        {
            try
            {
                await _services.UpdateStatistic(documentId, update);
                return Ok("Update successfully!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
    }
}
