namespace DoctorService.Api.DTOs;

public record CreateDoctorRequest(
    string Name,
    string Department,
    string Specialization,
    int Experience,
    decimal Fee
);