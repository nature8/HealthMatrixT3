using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace AppointmentService.Api.Messaging;

public class RabbitMqEventPublisher : IEventPublisher, IAsyncDisposable
{
    private readonly Task<IConnection> _connectionTask;
    private readonly SemaphoreSlim _initLock = new(1, 1);
    private IChannel? _channel;

    public RabbitMqEventPublisher(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMq:Host"] ?? "localhost",
            UserName = configuration["RabbitMq:Username"] ?? "guest",
            Password = configuration["RabbitMq:Password"] ?? "guest"
        };

        _connectionTask = factory.CreateConnectionAsync();
    }

    private async Task<IChannel> GetChannelAsync()
    {
        if (_channel is not null) return _channel;

        await _initLock.WaitAsync();
        try
        {
            if (_channel is null)
            {
                var connection = await _connectionTask;
                _channel = await connection.CreateChannelAsync();
            }
        }
        finally
        {
            _initLock.Release();
        }

        return _channel;
    }

    public async Task PublishAsync<T>(T message, string exchangeName)
    {
        var channel = await GetChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout, durable: true);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(exchange: exchangeName, routingKey: "", body: body);
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
            await _channel.CloseAsync();

        var connection = await _connectionTask;
        await connection.CloseAsync();
    }
}