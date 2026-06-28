using AppointmentService.Api.Data;
using AppointmentService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Api.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppointmentDbContext _context;

    public AppointmentRepository(AppointmentDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id) =>
        await _context.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.AppointmentId == id);

    public async Task<List<Appointment>> GetAllAsync() =>
        await _context.Appointments.AsNoTracking().ToListAsync();

    public async Task<bool> CancelAsync(Guid id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment is null) return false;

        appointment.Status = AppointmentStatus.Cancelled;
        await _context.SaveChangesAsync();
        return true;
    }
}