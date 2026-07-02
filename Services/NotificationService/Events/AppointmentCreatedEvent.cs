namespace NotificationService.Events
{
    public class AppointmentCreatedEvent
    {
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime AppointmentDate { get; set; }
    }
}