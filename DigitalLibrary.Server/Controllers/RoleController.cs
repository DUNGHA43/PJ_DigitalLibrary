using DigitalLibrary.Server.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRolesServices _services;

        public RoleController(IRolesServices services)
        {
            _services = services;
        }

        [HttpGet("getallroles")]
        public async Task<IActionResult> GetAllCategoriseAsync()
        {
            var nations = await _services.GetAllRolesAsync();

            if (nations == null)
            {
                return NotFound(new { message = "Không tìm thấy vai trò nào." });
            }

            return Ok(nations);
        }
    }
}
