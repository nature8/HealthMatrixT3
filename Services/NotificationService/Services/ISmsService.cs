namespace NotificationService.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}