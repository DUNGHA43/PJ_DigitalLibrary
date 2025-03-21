using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1, stafflv2")]
        public async Task<IActionResult> GetAllSubjectsAsync([FromQuery] int pageNumber = 1, int pageSize = 10, string searchName = "") {
            var (subjects, totalCount) = await _service.GetAllSubjectsAsync(pageNumber, pageSize, searchName);

            if (subjects == null || !subjects.Any())
            {
                return NotFound(new { message = "Không tìm thấy chủ đề nào!" });
            }

            var response = new
            {
                Data = subjects,
                TotalRecords = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return Ok(response);
        }

        [HttpGet("getall-noqueries")]
        [Authorize]
        public async Task<IActionResult> GetAllSubjectsAsync()
        {
            var subjects = await _service.GetAllSubjectsAsync();
            if (subjects == null)
            {
                return NotFound("No records?");
            }
            return Ok(subjects);
        }

        [HttpPost("addsubject")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddSubjectAsync([FromBody] SubjectDTO subjectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var subject = new Subjects()
            {
                subjectname = subjectDTO.subjectname,
                createdate = DateTime.Now,
                status = subjectDTO.status
            };

            if (await _service.FindSubjectByNameAsync(subjectDTO.subjectname!) != null) {
                throw new ArgumentException($"Subject with {subjectDTO.subjectname} already exist!");
            }

            await _service.AddSubjectAsync(subject);
            return Ok(new { data = subject, message = "Add success!"});
        }

        [HttpPut("editsubject")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> EditSubjectAsync([FromBody] SubjectDTO subjectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSubject = await _service.FindSubjectByIdAsync(subjectDTO.id);
            if (existingSubject == null) {
                throw new ArgumentException($"Subject with id {subjectDTO.id} dose not exist!");
            }

            existingSubject.subjectname = subjectDTO.subjectname ?? existingSubject.subjectname;
            existingSubject.status = subjectDTO.status;

            await _service.UpdateSubjectAsync(existingSubject);
            return Ok(new { data = existingSubject, message = "Edit success!" });
        }

        [HttpDelete("deletesubject")]
        [Authorize(Roles = "admin, stafflv1")]
        public async  Task<IActionResult> DeleteSubjectAsync([FromBody] int id)
        {

            var existingSubject = await _service.FindSubjectByIdAsync(id);
            if (existingSubject == null)
            {
                throw new ArgumentException($"Subject with id {id} dose not exist!");
            }

            await _service.DeleteSubjectAsync(id);
            return Ok(new { data = "Delete success!" });
        }

        [HttpDelete("deletemulti-subjects")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> DeleteMultiSubjectsAsync([FromBody] List<int> subjectIds)
        {
            await _service.DeleteMultipleSubjectsAsync(subjectIds);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
