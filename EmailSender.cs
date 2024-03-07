
using Microsoft.AspNetCore.Identity.UI.Services;
//This Class is to solve the problem of sending email
namespace TechLife
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email in future
            return Task.CompletedTask;
        }

    }
}
