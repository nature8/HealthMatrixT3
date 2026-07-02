using MassTransit;
using Microsoft.EntityFrameworkCore;
using NotificationService.Consumers;
using NotificationService.Data;
using NotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server DbContext
builder.Services.AddDbContext<NotificationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmsService, SmsService>();

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    // Register Consumers
    x.AddConsumer<AppointmentCreatedConsumer>();
    x.AddConsumer<AppointmentCancelledConsumer>();
    x.AddConsumer<BillGeneratedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            builder.Configuration["RabbitMQ:Host"],
            builder.Configuration["RabbitMQ:VirtualHost"],
            h =>
            {
                h.Username(builder.Configuration["RabbitMQ:Username"]!);
                h.Password(builder.Configuration["RabbitMQ:Password"]!);
            });

        // Appointment Created Queue
        cfg.ReceiveEndpoint("appointment-created-queue", e =>
        {
            e.ConfigureConsumer<AppointmentCreatedConsumer>(context);
        });

        // Appointment Cancelled Queue
        cfg.ReceiveEndpoint("appointment-cancelled-queue", e =>
        {
            e.ConfigureConsumer<AppointmentCancelledConsumer>(context);
        });

        // Bill Generated Queue
        cfg.ReceiveEndpoint("bill-generated-queue", e =>
        {
            e.ConfigureConsumer<BillGeneratedConsumer>(context);
        });
    });
});

// CORS (Optional)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Apply Migrations Automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
                  .GetRequiredService<NotificationDbContext>();

    db.Database.Migrate();
}

// Configure HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
