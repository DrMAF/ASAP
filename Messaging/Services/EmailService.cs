using System.Net;
using System.Net.Mail;
using Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Messaging.Services
{
    public class EmailService : IEmailService
    {
        readonly EmailSettings _emailSettings;
        readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string subject, string body, string receiver)
        {
            try
            {
                if (string.IsNullOrEmpty(body) || string.IsNullOrEmpty(receiver))
                {
                    return;
                }

                var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_emailSettings.SenderName, _emailSettings.SenderSecret)
                };

                var message = new MailMessage(_emailSettings.SenderName, receiver, subject, body);

                message.IsBodyHtml = true;

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in SendEmailAsync {ex} => {ex.Message}");
            }
        }
    }
}
