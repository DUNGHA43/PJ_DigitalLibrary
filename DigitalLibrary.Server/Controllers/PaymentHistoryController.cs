using DigitalLibrary.Server.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentHistoryController : Controller
    {
        private readonly IPaymentHistoryServices _services;

        public PaymentHistoryController(IPaymentHistoryServices services)
        {
            _services = services;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> GetAllPaymentHistorysAsync()
        {
            var paymentHistorys = await _services.GetAllPaymentHistorysAsync();
            if (paymentHistorys == null || !paymentHistorys.Any())
            {
                return NotFound("No payment history found.");
            }
            return Ok(paymentHistorys); 
        }

        [HttpPost("addhistoryandupdateplan")]
        [Authorize]
        public async Task<IActionResult> AddPaymentHistoryAsync([FromQuery] int userId, int planId)
        {
            try
            {
                await _services.AddPaymentHistoryAsync(userId, planId);
                return Ok("Payment history added and subscription plan updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }   
    }
}
