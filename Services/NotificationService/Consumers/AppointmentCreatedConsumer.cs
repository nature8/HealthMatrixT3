using MassTransit;
using NotificationService.Data;
using NotificationService.Events;
using NotificationService.Models;
using NotificationService.Services;
using Contracts.Events;

namespace NotificationService.Consumers
{
    public class AppointmentCreatedConsumer :
        IConsumer<AppointmentCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly NotificationDbContext _context;

        public AppointmentCreatedConsumer(
            IEmailService emailService,
            ISmsService smsService,
            NotificationDbContext context)
        {
            _emailService = emailService;
            _smsService = smsService;
            _context = context;
        }

        public async Task Consume(
            ConsumeContext<AppointmentCreatedEvent> context)
        {
            var message =
                $"Appointment booked on {context.Message.AppointmentDate}";

            await _emailService.SendEmailAsync(
                context.Message.Email,
                "Appointment Confirmation",
                message);

            await _smsService.SendSmsAsync(
                context.Message.PhoneNumber,
                message);

            var notification = new Notification
            {
                PatientName = context.Message.PatientName,
                Email = context.Message.Email,
                PhoneNumber = context.Message.PhoneNumber,
                Message = message,
                Type = "Appointment",
                SentDate = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}