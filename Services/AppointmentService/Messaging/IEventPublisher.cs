namespace AppointmentService.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<T>(T message, string exchangeName);
}
