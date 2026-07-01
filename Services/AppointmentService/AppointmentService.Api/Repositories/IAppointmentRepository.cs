using AppointmentService.Api.Models;

namespace AppointmentService.Api.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<List<Appointment>> GetAllAsync();
    Task<bool> CancelAsync(Guid id);
}