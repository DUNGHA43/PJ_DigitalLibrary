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
        public async Task<IActionResult> GetAllSubjectsAsync() { 
            var Subjects = await _service.GetAllSubjectsAsync();

            if (Subjects == null)
            {
                return NotFound("No records exist!");
            }

            return Ok(Subjects);
        }

        [HttpPost("addSubject")]
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
                status = true
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
        public async  Task<IActionResult> DeleteSubjectAsync(int id)
        {

            var existingSubject = await _service.FindSubjectByIdAsync(id);
            if (existingSubject == null)
            {
                throw new ArgumentException($"Subject with id {id} dose not exist!");
            }

            await _service.DeleteSubjectAsync(id);
            return Ok(new { data = "Delete success!" });
        }
    }
}
