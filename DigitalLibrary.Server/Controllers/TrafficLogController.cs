using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficLogController : Controller
    {
        private readonly ITrafficLogServices _services;
        private readonly HttpClient _httpClient;

        public TrafficLogController(ITrafficLogServices services, HttpClient httpClient)
        {
            _services = services;
            _httpClient = httpClient;
        }

        [HttpPost("log")]
        public async Task<IActionResult> LogTraffic([FromBody] TrafficLogDTO log)
        {
            if (log == null)
            {
                return BadRequest("Log cannot be null");
            }

            var trafficLog = new TrafficLog
            {
                url = log.url,
                useragent = log.useragent,
                ipaddress = log.ipaddress,
                createdate = DateTime.UtcNow
            };

            await _services.LogTraffic(trafficLog);
            return Ok();
        }

        [HttpGet("get-ip")]
        public async Task<IActionResult> GetIp()
        {
            try
            {
                var ip = await _httpClient.GetStringAsync("https://api.ipify.org");

                return Ok(ip);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
