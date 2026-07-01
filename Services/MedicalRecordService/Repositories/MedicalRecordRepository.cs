using Microsoft.EntityFrameworkCore;
using MedicalRecordService.Data;
using MedicalRecordService.Models;

namespace MedicalRecordService.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly MedicalRecordDbContext _context;

        public MedicalRecordRepository(MedicalRecordDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MedicalRecord>> GetByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Where(r => r.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<MedicalRecord?> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords
                .FirstOrDefaultAsync(r => r.RecordId == id);
        }

        public async Task UpdateAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}