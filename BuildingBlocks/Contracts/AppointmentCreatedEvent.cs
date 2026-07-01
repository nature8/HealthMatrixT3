namespace BuildingBlocks.Contracts.Events;

public record AppointmentCreatedEvent(
    Guid AppointmentId,
    int PatientId,
    int DoctorId,
    string PatientEmail,
    DateTime AppointmentDate
);