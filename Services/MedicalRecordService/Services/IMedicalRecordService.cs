using MedicalRecordService.DTOs;
using MedicalRecordService.Models;

namespace MedicalRecordService.Services
{
    public interface IMedicalRecordService
    {
        Task CreateAsync(CreateMedicalRecordDto dto);

        Task<List<MedicalRecord>> GetByPatientIdAsync(int patientId);

        Task<MedicalRecord?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, CreateMedicalRecordDto dto);

        Task<bool> DeleteAsync(int id);
    }
}