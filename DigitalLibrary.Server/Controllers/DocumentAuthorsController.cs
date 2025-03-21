using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentAuthorsController : Controller
    {
        private readonly IDocumentAuthorServices _service;

        public DocumentAuthorsController(IDocumentAuthorServices services)
        {
            _service = services;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> GetAllDocumentAuthorsAsync([FromQuery] int documentId)
        {
            var documentAuthors = await _service.GetAllDocumentAuthorsAsync(documentId);

            return Ok(documentAuthors);
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddDocumentAuthorsAsync([FromBody] DocumentAuthorDTO documentAuthorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentAuthor = new DocumentAuthors()
            {
                documentid = documentAuthorDTO.documentid,
                authorid = documentAuthorDTO.authorid,
            };

            await _service.AddDocumentAuthorAsync(documentAuthor);
            return Ok(new { data = documentAuthor, message = "Add success!" });
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> DeleteDocumentAuthorsAsync([FromBody] DocumentAuthorDTO documentSubjectDTO)
        {
            _service.DeleteDocumentAuthor(documentSubjectDTO.documentid, documentSubjectDTO.authorid);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
