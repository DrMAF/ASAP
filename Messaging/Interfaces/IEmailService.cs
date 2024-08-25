namespace Messaging.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body, string receiver);
    }
}