using DigitalLibrary.Server.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionsController : Controller
    {
        private readonly IUserSubscriptionServices _services;

        public UserSubscriptionsController(IUserSubscriptionServices services)
        {
            _services = services;
        }

        [HttpGet("getusersubscriptionsbyuserid")]
        [Authorize]
        public async Task<IActionResult> GetUserSubscriptionsByUserId([FromQuery] int userId)
        {
            var userSubscriptions = await _services.FindUserSubscriptionsByUserId(userId);
            return Ok(userSubscriptions);
        }
    }
}
