using MassTransit;
using BuildingBlocks.Contracts.Events;

namespace NotificationService.Consumers;

public class AppointmentCancelledConsumer :
    IConsumer<AppointmentCancelledEvent>
{
    public async Task Consume(
        ConsumeContext<AppointmentCancelledEvent> context)
    {
        Console.WriteLine(
            $"Appointment cancelled for {context.Message.PatientEmail}");

        await Task.CompletedTask;
    }
}