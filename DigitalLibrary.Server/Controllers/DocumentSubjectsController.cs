using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentSubjectsController : Controller
    {
        private readonly IDocumentSubjectsServices _service;

        public DocumentSubjectsController(IDocumentSubjectsServices services)
        {
            _service = services;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> GetAllDocumentSubjectAsync([FromQuery] int documentId)
        {
            var documentSubject = await _service.GetAllDocumentSubjectsAsync(documentId);

            return Ok(documentSubject);
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddDocumentSubjectAsync([FromBody] DocumentSubjectDTO documentSubjectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentsubject = new DocumentSubject()
            {
                documentid = documentSubjectDTO.documentid,
                subjectid = documentSubjectDTO.subjectid,
            };

            await _service.AddDocumentSubjectAsync(documentsubject);
            return Ok(new { data = documentsubject, message = "Add success!" });
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> DeleteDocumentSubjectAsync([FromBody] DocumentSubjectDTO documentSubjectDTO)
        {
            _service.DeleteDocumentSubject(documentSubjectDTO.documentid, documentSubjectDTO.subjectid);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
