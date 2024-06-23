using FluentEmail.Core;
using System.Net.Mail;

namespace TechLife.EmailServices
{
    public class FluentEmailService : IFluentEmailService
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentEmailService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendEmailForOrderAsync(string recipientEmail, string recipientName, string subject, string description)
        {
            if (!IsValidEmail(recipientEmail))
            {
                throw new FormatException("The specified string is not in the form required for an e-mail address.");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer.To(recipientEmail, recipientName)
                                  .Subject(subject)
                                  .Body(description);
                var response = await email.SendAsync();

                if (!response.Successful)
                {
                    throw new Exception($"Failed to send email: {string.Join(", ", response.ErrorMessages)}");
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
