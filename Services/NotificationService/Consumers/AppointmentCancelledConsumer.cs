using Contracts.Events;
using NotificationService.Services;
using MassTransit;
public class AppointmentCancelledConsumer :
    IConsumer<AppointmentCancelledEvent>
{
    private readonly IEmailService _emailService;

    public AppointmentCancelledConsumer(
        IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(
        ConsumeContext<AppointmentCancelledEvent> context)
    {
        await _emailService.SendEmailAsync(
            context.Message.Email,
            "Appointment Cancelled",
            "Your appointment has been cancelled.");
    }
}