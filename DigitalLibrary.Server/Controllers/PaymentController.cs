using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PaymentController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(PayOS payOS, ILogger<PaymentController> logger)
        {
            _payOS = payOS;
            _logger = logger;
        }

        [HttpPost("payos_transfer_handler")]
        public IActionResult PayOsTransferHandler([FromBody] WebhookType body)
        {
            try
            {
                WebhookData data = _payOS.verifyPaymentWebhookData(body);

                _logger.LogInformation("Webhook received: {@data}", data);

                if (data.description == "Ma giao dich thu nghiem" || data.description == "VQRIO123")
                {
                    return Ok(new Response { error = 0, message = "Giao dịch test thành công", data = null });
                }

                return Ok(new Response{ error = 0, message = "Giao dịch thành công", data = data });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lỗi khi xử lý webhook");
                return BadRequest(new Response{ error = -1, message = "Webhook không hợp lệ", data = null });
            }
        }
    }
}
