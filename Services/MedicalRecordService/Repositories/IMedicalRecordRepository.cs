using MedicalRecordService.Models;

namespace MedicalRecordService.Repositories
{
    public interface IMedicalRecordRepository
    {
        Task AddAsync(MedicalRecord record);

        Task<List<MedicalRecord>> GetByPatientIdAsync(int patientId);

        Task<MedicalRecord?> GetByIdAsync(int id);

        Task UpdateAsync(MedicalRecord record);

        Task DeleteAsync(MedicalRecord record);
    }
}