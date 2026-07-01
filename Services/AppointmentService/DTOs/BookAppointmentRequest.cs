namespace AppointmentService.DTOs;

public record BookAppointmentRequest(
    int PatientId,
    int DoctorId,
    string PatientEmail,
    DateTime AppointmentDate
);
