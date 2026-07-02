namespace NotificationService.Services
{
    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;

        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger;
        }

        public async Task SendSmsAsync(
            string number,
            string message)
        {
            _logger.LogInformation(
                $"SMS Sent to {number}: {message}");

            await Task.CompletedTask;
        }
    }
}