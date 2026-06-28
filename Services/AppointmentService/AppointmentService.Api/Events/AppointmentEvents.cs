namespace AppointmentService.Api.Events;

// Published by: Appointment Service
// Consumed by: Notification Service, Billing Service
public record AppointmentCreatedEvent(
    Guid AppointmentId,
    int PatientId,
    int DoctorId,
    DateTime AppointmentDate
);

// Published by: Appointment Service
// Consumed by: Notification Service
public record AppointmentCancelledEvent(
    Guid AppointmentId
);