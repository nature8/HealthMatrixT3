namespace Contracts.Events
{
    public class AppointmentCancelledEvent
    {
        public int AppointmentId { get; set; }
        public string Email { get; set; }
        public string PatientName { get; set; }
    }
}