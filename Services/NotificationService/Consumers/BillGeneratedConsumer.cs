using Contracts.Events;
using NotificationService.Services;
using MassTransit;
public class BillGeneratedConsumer : IConsumer<BillGeneratedEvent>
{
    private readonly IEmailService _emailService;

    public BillGeneratedConsumer(
        IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<BillGeneratedEvent> context)
    {
        await _emailService.SendEmailAsync(
            context.Message.Email,
            "Bill Generated",
            $"Your bill amount is ₹{context.Message.Amount}");
    }
}