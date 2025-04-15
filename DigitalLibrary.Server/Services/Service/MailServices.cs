using DigitalLibarary.Shared.DTO;
using DigitalLibrary.Shared.DTO;
using System.Net.Http;

namespace DigitalLibrary.Server.Services.Interface
{
    public class MailServices : IMailServices
    {
        private readonly HttpClient _httpClient;
        public string _apiKey;

        public MailServices(HttpClient httpClient, IConfiguration configuration)
        {
            _apiKey = configuration["BrevoKey:ApiKey"]; ;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        }

        public async Task<bool> SendEmailAsync(EmailSenderDTO emailSender)
        {
            try
            {
                if (string.IsNullOrEmpty(_apiKey))
                {
                    throw new Exception("API key is not set.");
                }
                var senderEmail = new
                {
                    sender = new { name = emailSender.FromName, email = emailSender.FromEmail },
                    to = new[] { new { email = emailSender.ToEmail } },
                    subject = emailSender.Subject,
                    htmlContent = $"{emailSender.Body} : {emailSender.Code}"
                };

                var response = await _httpClient.PostAsJsonAsync("https://api.brevo.com/v3/smtp/email", senderEmail);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
