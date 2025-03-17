using DigitalLibrary.Server.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationController : Controller
    {
        private readonly INationServices _services;

        public NationController(INationServices services)
        {
            _services = services;
        }

        [HttpGet("getallnations")]
        public async Task<IActionResult> GetAllCategoriseAsync()
        {
            var nations = await _services.GetAllNationsAsync();

            if (nations == null)
            {
                return NotFound(new { message = "Không tìm thấy thể loại nào." });
            }

            return Ok(nations);
        }
    }
}
