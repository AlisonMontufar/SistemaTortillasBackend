using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Services.Auth;
using Tortilleria.Infrastructure.Configuration;

namespace Tortilleria.Infrastructure.Services.Auth
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var client = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_settings.FromEmail, _settings.AppPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.FromEmail, "TortillasApp"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

            
                if (!string.IsNullOrWhiteSpace(_settings.ReplyToEmail))
                {
                    mailMessage.ReplyToList.Add(new MailAddress(_settings.ReplyToEmail));
                }

                try
                {
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("✅ Correo enviado correctamente.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"❌ Error SMTP: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
