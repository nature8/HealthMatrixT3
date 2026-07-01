using DoctorService.Api.Data;
using DoctorService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Api.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly DoctorDbContext _context;

    public DoctorRepository(DoctorDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetAllAsync() =>
        await _context.Doctors.AsNoTracking().ToListAsync();

    public async Task<Doctor?> GetByIdAsync(int id) =>
        await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.DoctorId == id);

    public async Task<Doctor> CreateAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }
}