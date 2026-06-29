using BuildingBlocks.Contracts.Events;
using MassTransit;

namespace NotificationService.Consumers;

public class AppointmentCreatedConsumer :
    IConsumer<AppointmentCreatedEvent>
{
    public async Task Consume(
        ConsumeContext<AppointmentCreatedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine(
            $"Appointment booked for {message.PatientEmail}");

        await Task.CompletedTask;
    }
}