using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace TelecomLayer
{
    public class EmailSenderService : IEmailSenderService
    {
        public EmailSenderService()
        {

        }

        public async Task SendEmailAsync(string subject, string body, string receiver)
        {
            if (string.IsNullOrEmpty(body) || receiver == null || !receiver.Any())
            {
                return;
            }

            string from = "drmaf5000@yahoo.com";
            string passwd = "fzugshbxfutpuszd";

            var client = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(from, passwd)
            };

            var message = new MailMessage(from, receiver, subject, body);

            await client.SendMailAsync(message);
        }
    }
}
