using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _service;

        public DocumentController(IDocumentService service)
        {
            _service = service;
        }

        [HttpPost("adddocument")]
        public async Task<IActionResult> AddDocumentAsync([FromForm] DocumentsDTO documentDTO, IFormFile dcmFile, IFormFile imgFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var document = new Documents()
            {
                title = documentDTO.title,
                publisher = documentDTO.publisher,
                imagepath = documentDTO.imagepath,
                imageurl = documentDTO.imageurl,
                filepath = documentDTO.filepath,
                fileurl = documentDTO.fileurl,
                uploadedby = documentDTO.uploadedby,
                description = documentDTO.description,
                createdate = DateTime.Now,
                status = true,
            };

            await _service.AddDocumentAsync(document, dcmFile, imgFile);

            return Ok(new {data = document, message = "Add success!" });
        }
    }
}
