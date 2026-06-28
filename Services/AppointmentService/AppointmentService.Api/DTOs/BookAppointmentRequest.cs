namespace AppointmentService.Api.DTOs;

public record BookAppointmentRequest(
    int PatientId,
    int DoctorId,
    DateTime AppointmentDate
);