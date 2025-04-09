using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Shared.DTO;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(PayOS payOS, IHttpContextAccessor httpContextAccessor, ILogger<CheckoutController> logger)
        {
            _payOS = payOS;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpPost("create-payment-link")]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                var items = new List<ItemData>
            {
                new ItemData("Mì tôm hảo hảo ly", 1, 10000)
            };

                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host}";

                var paymentData = new PaymentData(
                    orderCode,
                    10000,
                    "Thanh toán đơn hàng",
                    items,
                    $"{baseUrl}/cancel",
                    $"{baseUrl}/success"
                );

                var createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(new Response{error = 0, message = "Tạo liên kết thanh toán thành công", data = new
                {
                    orderCode = orderCode,
                    checkoutUrl = createPayment.checkoutUrl
                }});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo liên kết thanh toán");
                return StatusCode(500, new Response { error = -1, message = "Tạo liên kết thanh toán thất bại", data = null });
            }
        }
    }
}