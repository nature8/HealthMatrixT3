namespace AppointmentService.Api.Models;

public enum AppointmentStatus
{
    Booked,
    Completed,
    Cancelled
}

public class Appointment
{
    public Guid AppointmentId { get; set; } = Guid.NewGuid();
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}