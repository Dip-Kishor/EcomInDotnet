using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EcommerceDotnet.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EcommerceDotnet.Services
{
    public class ContactService : IContactService
    {
        private readonly EmailSettings _emailSettings;

        // Constructor to inject the email settings
        public ContactService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmail(string toEmail, string subject, string messageBody)
        {
            var mimeMessage = new MimeMessage();

            // Set who the email is from and who it's being sent to
            mimeMessage.From.Add(new MailboxAddress("Furni User", _emailSettings.SmtpUser));
            mimeMessage.To.Add(new MailboxAddress("Furni Admin", toEmail));

            // Set the email subject and message body
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = messageBody };

            // Send the email using SMTP
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, true); // SSL true

                // Use email settings for authentication
                await client.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPass);

                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
