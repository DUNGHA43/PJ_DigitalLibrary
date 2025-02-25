using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _service;

        public ReviewsController(IReviewsService service)
        {
            _service = service;
        }

        [HttpGet("getallreviews")]
        [Authorize]
        public async Task<IActionResult> GetAllReviewsAsync() 
        {
            var reviews = await _service.GetAllReviewsAsync();

            return Ok(reviews);
        }

        [HttpGet("getallreviewsbyuser")]
        [Authorize]
        public async Task<IActionResult> GetAllReviewsByUserAsync(int userId)
        {
            var reviews = await _service.FindReviewsByUserIdAsync(userId);
            return Ok(reviews);
        }

        [HttpGet("getallreviewsbydocument")]
        [Authorize]
        public async Task<IActionResult> GetAllReviewsByDocumentAsync(int documentId)
        {
            var reviews = await _service.FindReviewsByDocumentIdAsync(documentId);
            return Ok(reviews);
        }

        [HttpPost("addreview")]
        [Authorize]
        public async Task<IActionResult> AddReviewAsync([FromBody] ReviewsDTO reviewsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = new Reviews()
            {
                userid = reviewsDTO.userid,
                documentid = reviewsDTO.documentid,
                rating = reviewsDTO.rating,
                comment = reviewsDTO.comment,
                createdate = reviewsDTO.createdate,
                status = reviewsDTO.status,
            };

            await _service.AddReviewAsync(review);
            return Ok(new {data = review , message = "Add success!"});
        }

        [HttpPut("editreview")]
        [Authorize]
        public async Task<IActionResult> EditReviewAsync([FromBody] ReviewsDTO reviewsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReview = await _service.FindReviewByIdAsync(reviewsDTO.id);

            existingReview.rating = reviewsDTO.rating ?? existingReview.rating;
            existingReview.comment = reviewsDTO.comment ?? existingReview.comment;
            
            await _service.EditReviewAsync(existingReview);
            return Ok(new { data = existingReview, message = "Edit success!" });
        }

        [HttpDelete("deletereviews")]
        [Authorize]
        public async Task<IActionResult> DeleteReviewAsync(int id)
        {
            var existingReviews = await _service.FindReviewByIdAsync(id);

            await _service.DeleteReviewAsync(id);
            return Ok(new { data = existingReviews, message = "Delete success!" });
        }
    }
}
