using DoctorService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Api.Data;

public class DoctorDbContext : DbContext
{
    public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors => Set<Doctor>();
}