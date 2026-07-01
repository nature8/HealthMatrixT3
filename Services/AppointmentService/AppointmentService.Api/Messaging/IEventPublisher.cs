namespace AppointmentService.Api.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<T>(T message, string exchangeName);
}