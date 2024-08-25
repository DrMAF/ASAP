
namespace TelecomLayer
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string subject, string body, string receiver);
    }
}