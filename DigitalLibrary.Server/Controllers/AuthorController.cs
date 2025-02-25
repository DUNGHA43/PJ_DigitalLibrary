using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Server.Services.Service;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorsService _service;

        public AuthorController(IAuthorsService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1, stafflv2")]
        public async Task<IActionResult> GetAllAuthorsAsync()
        {
            var authors = await _service.GetAllAuthorsAsync();

            if(authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }

        [HttpPost("addauthor")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> AddAuthorAsync([FromBody] AuthorsDTO authorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Authors()
            {
                fullname = authorDTO.fullname,
                birthday = authorDTO.birthday,
                nationalityid = authorDTO.nationalityid,
                description = authorDTO.description,
                createdate = DateTime.Now,
                status = true,
            };

            await _service.AddAuthorAsync(author);
            return Ok(new { data = author, message = "Add success!"});
        }

        [HttpPut("editauthor")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> EditAuthorAsync([FromBody] AuthorsDTO authorsDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAuthor = await _service.FindAuthorByIdAsync(authorsDTO.id);

            if(existingAuthor == null)
            {
                return NotFound($"Author with id {authorsDTO.id} not found!");
            }

            existingAuthor.fullname = authorsDTO.fullname ?? existingAuthor.fullname;
            existingAuthor.birthday = authorsDTO.birthday ?? existingAuthor.birthday;
            existingAuthor.nationalityid = authorsDTO.nationalityid ?? existingAuthor.nationalityid;
            existingAuthor.description = authorsDTO.description ?? existingAuthor.description;
            existingAuthor.status = authorsDTO.status;

            await _service.UpdateAuthorAsync(existingAuthor);
            return Ok(new { data = existingAuthor, message = "Edit success!" });
        }

        [HttpDelete("deleteauthor")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> DeleteAuthorAsync(int id)
        {
            var existingAuthor = await _service.FindAuthorByIdAsync(id);
            if (existingAuthor == null)
            {
                return NotFound($"Author with id {id} not found!");
            }

            await _service.DeleteAuthorAsync(id);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
