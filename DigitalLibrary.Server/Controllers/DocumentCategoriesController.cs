using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentCategoriesController : Controller
    {
        private readonly IDocumentCategoriesServices _service;

        public DocumentCategoriesController(IDocumentCategoriesServices services)
        {
            _service = services;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> GetAllDocumentCategoriesAsync([FromQuery] int documentId)
        {
            var documentCategories = await _service.GetAllDocumentCategoriessAsync(documentId);

            return Ok(documentCategories);
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddDocumentCategoriesAsync([FromBody] DocumentCategoriesDTO documentCategoriesDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentCategory = new DocumentCategories()
            {
                documentid = documentCategoriesDTO.documentid,
                categoryid = documentCategoriesDTO.categoryid,
            };

            await _service.AddDocumentCategoriesAsync(documentCategory);
            return Ok(new { data = documentCategory, message = "Add success!" });
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> DeleteDocumentCategoriesAsync([FromBody] DocumentCategoriesDTO documentCategoriesDTO)
        {
            _service.DeleteDocumentCategories(documentCategoriesDTO.documentid, documentCategoriesDTO.categoryid);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
