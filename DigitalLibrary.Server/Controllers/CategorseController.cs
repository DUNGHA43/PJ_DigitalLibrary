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
        private readonly ICategoriseServices _service;

        public CategorseController(ICategoriseServices service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1, stafflv2")]
        public async Task<IActionResult> GetAllCategoriseAsync([FromQuery] int pageNumber = 1, int pageSize = 10, string searchName = "")
        {
            var (categories, totalCount) = await _service.GetAllCategoriesAsync(pageNumber, pageSize, searchName);

            if (categories == null || !categories.Any())
            {
                return NotFound(new { message = "Không tìm thấy thể loại nào." });
            }

            var response = new
            {
                Data = categories,
                TotalRecords = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double) totalCount / pageSize)
            };

            return Ok(response);
        }

        [HttpGet("getall-noqueries")]
        [Authorize]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _service.GetAllCategoriesAsync();
            if (categories == null)
            {
                return NotFound("No records?");
            }
            return Ok(categories);
        }

        [HttpPost("addcategory")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoriesDTO categoriseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Categories()
            {
                catename = categoriseDTO.catename,
                createdate = DateTime.Now,
                status = categoriseDTO.status,
            };

            await _service.AddCategoryAsync(category);

            return Ok(new { data = category, message = "Add success!" });
        }

        [HttpPut("editcategory")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> EditCategoryAsync([FromBody] CategoriesDTO categoriseDTO)
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
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] int id)
        {
            var existingCate = await _service.FindCategoryByIdAsync(id);
            if (existingCate == null)
            {
                return NotFound($"Category with id {id} not found!");
            }

            await _service.DeleteCategoryAsync(id);
            return Ok(new { data = "Delete Success!" });
        }

        [HttpDelete("deletemulti-category")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] List<int> categoryIds)
        {
            await _service.DeleteMultipleAsync(categoryIds);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
