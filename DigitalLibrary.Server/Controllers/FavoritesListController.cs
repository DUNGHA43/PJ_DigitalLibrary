using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesListController : Controller
    {
        private readonly IFavoListServies _services;

        public FavoritesListController(IFavoListServies favoListServices)
        {
            _services = favoListServices;
        }

        [HttpGet("getfavolist")]
        [Authorize]
        public async Task<IActionResult> GetFavoListAsync([FromQuery] FavoListDTO favoListDTO)
        {
            var favoList = await _services.FindFavoListAsync(favoListDTO.documentid, favoListDTO.userid);
            if (favoList == null)
            {
                return NotFound("No favorites found for this user");
            }
            return Ok(favoList);
        }

        [HttpGet("getfavolistbyuser")]
        [Authorize]
        public async Task<IActionResult> GetFavoListByUserAsync([FromQuery] int userId)
        {
            var favoList = await _services.GetFavoListByUserAsync(userId);
            if (favoList == null)
            {
                return NotFound("No favorites found for this user!");
            }
            return Ok(favoList);
        }

        [HttpPost("addfavolist")]
        [Authorize]
        public async Task<IActionResult> AddFavoListAsync([FromBody] FavoListDTO favoListDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var favoList = new FavoList
            {
                documentid = favoListDTO.documentid,
                userid = favoListDTO.userid
            };

            try
            {
                await _services.AddFavoListAsync(favoList);
                return Ok("Added to favorites list");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteFavoListAsync([FromBody] FavoListDTO favoListDTO)
        {
            await _services.DeleteFavoListAsync(favoListDTO.documentid, favoListDTO.userid);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
