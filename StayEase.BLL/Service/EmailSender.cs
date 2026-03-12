using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace StayEase.BLL.Service
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("hmoodabuomar2018@gmail.com", "kmne kavl vxok heud")
            };

            return client.SendMailAsync(
                new MailMessage(from: "hmoodabuomar2018@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                { IsBodyHtml = true });
        }
    }

}
