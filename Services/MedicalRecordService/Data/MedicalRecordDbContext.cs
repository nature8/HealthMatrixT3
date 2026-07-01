using Microsoft.EntityFrameworkCore;
using MedicalRecordService.Models;

namespace MedicalRecordService.Data
{
    public class MedicalRecordDbContext : DbContext
    {
        public MedicalRecordDbContext(DbContextOptions<MedicalRecordDbContext> options)
            : base(options)
        {
        }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }
    }
}