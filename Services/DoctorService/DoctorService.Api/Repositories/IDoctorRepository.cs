using DoctorService.Api.Models;

namespace DoctorService.Api.Repositories;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task<Doctor> CreateAsync(Doctor doctor);
}