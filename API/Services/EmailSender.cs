
using DTO.Models.Auth;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string companyName, string email, string subject, string htmlMessage);
    }
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string companyName, string email, string subject, string message)
        {
            try
            {
                string company = companyName;
                string[] words = company.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Substring(0, 1);
                }
                company = string.Join("", words).ToLower();

                var SenderName = "";
                var SenderEmail = "";
                var SenderPassword = "";
                foreach (var sender in _emailSettings.senderDetails)
                {
                    if (sender.SenderName == company)
                    {
                        SenderName = sender.SenderName;
                        SenderEmail = sender.Sender;
                        SenderPassword = sender.Password;
                    }
                }

                // Credentials
                var credentials = new NetworkCredential(SenderEmail, SenderPassword);

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(SenderEmail, companyName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = _emailSettings.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = _emailSettings.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };

                // Send it...         
                client.Send(mail);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }

            return Task.CompletedTask;
        }

    }
}
