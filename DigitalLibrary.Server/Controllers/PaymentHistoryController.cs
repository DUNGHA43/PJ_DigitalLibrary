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

        [HttpGet("getrevenue")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> GetStatisticRevenueAsync([FromQuery] int? day = null, int? month = null, int? year = null)
        {
            var paymentHistorys = await _services.GetStatisticRevenueAsync(day, month, year);
            if (paymentHistorys == null || !paymentHistorys.Any())
            {
                return NotFound("No payment history found.");
            }
            return Ok(paymentHistorys); 
        }
        [HttpGet("getmonthlyrevenue")]
        [Authorize(Roles = "admin, stafflv1")]
        public async Task<IActionResult> GetMonthlyRevenueByYearAsync([FromQuery] int? year)
        {
            var paymentHistorys = await _services.GetMonthlyRevenueByYearAsync(year);
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
