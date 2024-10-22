
using GamerZone.MVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace GamerZone.Services
{
    public class EmailSender(IOptions<AdminCradentials> options) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var fromMail = options.Value.Email;
            var fromPassword = options.Value.Password;
            var message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;

            var smtClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };
            smtClient.Send(message);
        }
    }
}