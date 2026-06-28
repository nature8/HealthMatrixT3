using AppointmentService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Api.Data;

public class AppointmentDbContext : DbContext
{
    public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options) { }

    public DbSet<Appointment> Appointments => Set<Appointment>();
}