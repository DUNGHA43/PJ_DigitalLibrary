using DigitalLibarary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IMailServices
    {
        Task<bool> SendEmailAsync(EmailSenderDTO emailSender);
    }
}
