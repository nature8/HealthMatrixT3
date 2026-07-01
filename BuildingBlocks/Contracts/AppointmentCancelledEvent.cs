namespace BuildingBlocks.Contracts.Events;

public record AppointmentCancelledEvent(
    Guid AppointmentId,
    string PatientEmail
);