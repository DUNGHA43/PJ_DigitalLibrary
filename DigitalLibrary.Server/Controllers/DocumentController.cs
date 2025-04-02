using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : Controller
    {
        private readonly IDocumentServices _service;
        private readonly IWebHostEnvironment _env;

        public DocumentController(IDocumentServices service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env; 
        }

        [HttpGet("getdocument_noauthorize")]
        public async Task<IActionResult> GetDocumentNoAuthorizeAsync([FromQuery] int? subjectId = null, int? authorId = null, int? categoryId = null,
        string? accesslevel = null, string? searchName = null)
        {
            var documents = await _service.GetDocumentHomePageAsync(subjectId, authorId, categoryId, accesslevel, searchName);
            return Ok(documents);
        }

        [HttpGet("getdocumentfilter_noauthorize")]
        public async Task<IActionResult> GetDocumentsByFilter(
        [FromQuery] int? subjectId = null,
        [FromQuery] int? authorId = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] string? accessLevel = null,
        [FromQuery] string? searchName = null,
        [FromQuery] string? filterGroup = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            try
            {
                var documents = await _service.GetDocumentByFilterAsync(
                    subjectId, authorId, categoryId, accessLevel, searchName, filterGroup, page, pageSize
                );

                if (documents == null || !documents.Any())
                {
                    return NotFound("Không tìm thấy tài liệu nào.");
                }

                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getallinfodocuments")]
        [Authorize]
        public async Task<IActionResult> GetAllInfoDocumentsAsync([FromQuery] int pageNumber = 1, int pageSize = 10, string searchName = "")
        {
            var (documents, totalCount) = await _service.GetAllDocumentsAsync(pageNumber, pageSize, searchName);

            if (documents == null || !documents.Any())
            {
                return NotFound(new { message = "Không tìm thấy tài liệu nào." });
            }

            var response = new
            {
                Data = documents,
                TotalRecords = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return Ok(response);
        }

        [HttpGet("getdocumentbyid")]
        [Authorize]
        public async Task<IActionResult> GetDocumentById([FromQuery] int documentId)
        {
            var document = await _service.FindDocumentDetailByIdAsync(documentId);

            if(document == null)
            {
                return NotFound("document with id does not exist!");
            }

            return Ok(document);
        }

        [HttpGet("getimg/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDocumentImage(int id)
        {
            if (id <= 0)
                return BadRequest("ID tài liệu không hợp lệ!");

            var document = await _service.FindDocumentByIdAsync(id);
            if (document == null || string.IsNullOrEmpty(document.imageurl))
            {
                return NotFound("Không tìm thấy tài liệu hoặc tài liệu không có ảnh.");
            }

            var imagePath = Path.Combine("wwwroot/UploadImages", document.imageurl);
            Console.WriteLine($"🔍 Đường dẫn ảnh: {imagePath}");

            string fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/').Replace("/", "\\"));

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound($"Ảnh không tồn tại trên server: {fullPath}");
            }

            try
            {
                var imageBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(imageBytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi đọc ảnh: {ex.Message}");
                return StatusCode(500, "Lỗi khi tải ảnh.");
            }
        }

        [HttpGet("getpdf/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDocumentPdf(int id)
        {
            var document = await _service.FindDocumentByIdAsync(id);
            if (document == null || string.IsNullOrEmpty(document.fileurl))
            {
                return NotFound("Không tìm thấy tài liệu hoặc tài liệu không có ảnh.");
            }

            var imagePath = Path.Combine("wwwroot/UploadImages", document.fileurl);

            string fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/').Replace("/", "\\"));

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound($"File không tồn tại trên server: {fullPath}");
            }

            try
            {
                var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, "application/pdf", $"{document.title}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi tải tài liệu.");
            }
        }


        [HttpPost("adddocument")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddDocumentAsync([FromForm] DocumentsDTO documentDTO, IFormFile dcmFile, IFormFile? imgFile)
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
                accesslevel = documentDTO.accesslevel,
                createdate = DateTime.Now,
                status = documentDTO.status,
            };

            await _service.AddDocumentAsync(document, dcmFile, imgFile);

            return Ok(new {data = document, message = "Add success!" });
        }

        [HttpPut("editdocument")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> EditDocumentAsync([FromForm] DocumentsDTO documentDTO, IFormFile? dcmFile, IFormFile? imgFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDocument = await _service.FindDocumentByIdAsync(documentDTO.id);

            if (existingDocument == null)
            {
                return NotFound($"Document with id {documentDTO.id} not found!");
            }

            existingDocument.title = documentDTO.title ?? existingDocument.title;
            existingDocument.publisher = documentDTO.publisher ?? existingDocument.publisher;
            existingDocument.imagepath = documentDTO.imagepath ?? existingDocument.imagepath;
            existingDocument.imageurl = documentDTO.imageurl ?? existingDocument.imageurl;
            existingDocument.filepath = documentDTO.filepath ?? existingDocument.filepath;
            existingDocument.fileurl = documentDTO.fileurl ?? existingDocument.fileurl;
            existingDocument.uploadedby = documentDTO.uploadedby ?? existingDocument.uploadedby;
            existingDocument.description = documentDTO.description ?? existingDocument.description;
            existingDocument.accesslevel = documentDTO.accesslevel ?? existingDocument.accesslevel;
            existingDocument.status = documentDTO.status;

            await _service.UpdateDocumentAsync(existingDocument, dcmFile, imgFile);
            return Ok(new { data = existingDocument, message = "Edit success!" });
        }

        [HttpDelete("deletedocument")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDocumentAsync([FromBody] int id)
        {
            await _service.DeleteDocumentAsync(id);
            return Ok(new { message = "Delete success!" });
        }

        [HttpDelete("deletemulti-documents")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteMultipleUsersAsync([FromBody] List<int> documentIds)
        {
            await _service.DeleteMultipleDocumentsAsync(documentIds);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
