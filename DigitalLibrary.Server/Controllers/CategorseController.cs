using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorseController : Controller
    {
        private readonly ICategoriseService _service;

        public CategorseController(ICategoriseService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1, stafflv2")]
        public async Task<IActionResult> GetAllCategoriseAsync()
        {
            var categorise = await _service.GetAllCategorisesAsync();
            if (categorise == null)
            {
                return NotFound();
            }

            return Ok(categorise);
        }

        [HttpPost("addcategory")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoriseDTO categoriseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Categorise()
            {
                catename = categoriseDTO.catename,
                createdate = DateTime.Now,
                status = true
            };

            await _service.AddCategoryAsync(category);

            return Ok(new { data = category, message = "Add success!" });
        }

        [HttpPut("editcategory")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> EditCategoryAsync([FromBody] CategoriseDTO categoriseDTO)
        {
            if(!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var existingCate = await _service.FindCategoryByIdAsync(categoriseDTO.id);
            if (existingCate == null) {
                return NotFound($"Category with id {categoriseDTO.id} not found!");
            }

            existingCate.catename = categoriseDTO.catename ?? existingCate.catename;
            existingCate.status = categoriseDTO.status; 

            await _service.UpdateCategoryAsync(existingCate);
            return Ok(new { data = existingCate, message = "Add success!" });
        }

        [HttpDelete("deletecategory")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var existingCate = await _service.FindCategoryByIdAsync(id);
            if (existingCate == null)
            {
                return NotFound($"Category with id {id} not found!");
            }

            await _service.DeleteCategoryAsync(id);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
