namespace TechLife.EmailServices
{
    public interface IFluentEmailService 
    {
        Task SendEmailForOrderAsync(string recipientEmail, string recipientName, string subject, string description);
    }
}
