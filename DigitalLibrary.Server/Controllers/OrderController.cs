using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly ILogger<OrderController> _logger;

        public OrderController(PayOS payOS, ILogger<OrderController> logger)
        {
            _payOS = payOS;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] CreatePaymentLinkRequest body)
        {
            try
            {
                 int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                var items = new List<ItemData>
                {
                    new ItemData(body.productName, 1, body.price)
                };  

                var paymentData = new PaymentData(orderCode, body.price, body.description, items, body.cancelUrl, body.returnUrl);

                 CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(new Response {error = 0, message = "Tạo liên kết thanh toán thành công", data = createPayment });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo liên kết thanh toán");
                return StatusCode(500, new Response { error = -1, message = "Tạo liên kết thanh toán thất bại", data = null });
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            try
            {
                var paymentLinkInfo = await _payOS.getPaymentLinkInformation(orderId);
                return Ok(new Response{ error = 0, message = "Lấy thông tin đơn hàng thành công", data = paymentLinkInfo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy đơn hàng");
                return StatusCode(404, new Response { error = -1, message = "Không tìm thấy đơn hàng", data = null });
            }
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
        {
            try
            {
                var result = await _payOS.cancelPaymentLink(orderId);
                return Ok(new Response { error = 0, message = "Hủy đơn hàng thành công", data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi hủy đơn hàng");
                return StatusCode(500, new Response{ error = -1, message = "Hủy đơn hàng thất bại", data = null });
            }
        }

        [HttpPost("confirm-webhook")]
        public async Task<IActionResult> ConfirmWebhook([FromBody] ConfirmWebhook body)
        {
            try
            {
                await _payOS.confirmWebhook(body.webhook_url);
                return Ok(new Response { error = 0, message = "Xác nhận webhook thành công", data = null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xác nhận webhook");
                return StatusCode(500, new Response { error = -1, message = "Xác nhận webhook thất bại", data = null });
            }
        }

    }
}
