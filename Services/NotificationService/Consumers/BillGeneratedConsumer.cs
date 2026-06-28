using BuildingBlocks.Contracts.Events;
using MassTransit;

namespace NotificationService.Consumers;

public class BillGeneratedConsumer :
    IConsumer<BillGeneratedEvent>
{
    public async Task Consume(
        ConsumeContext<BillGeneratedEvent> context)
    {
        Console.WriteLine(
            $"Bill generated for {context.Message.PatientEmail}");

        await Task.CompletedTask;
    }
}