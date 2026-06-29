using MedicalRecordService.DTOs;
using MedicalRecordService.Models;
using MedicalRecordService.Repositories;

namespace MedicalRecordService.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repository;

        public MedicalRecordService(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateMedicalRecordDto dto)
        {
            var record = new MedicalRecord
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Diagnosis = dto.Diagnosis,
                Prescription = dto.Prescription,
                MedicalNotes = dto.MedicalNotes,
                VisitDate = dto.VisitDate
            };

            await _repository.AddAsync(record);
        }

        public async Task<List<MedicalRecord>> GetByPatientIdAsync(int patientId)
        {
            return await _repository.GetByPatientIdAsync(patientId);
        }

        public async Task<MedicalRecord?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, CreateMedicalRecordDto dto)
        {
            var record = await _repository.GetByIdAsync(id);

            if (record == null)
                return false;

            record.PatientId = dto.PatientId;
            record.DoctorId = dto.DoctorId;
            record.Diagnosis = dto.Diagnosis;
            record.Prescription = dto.Prescription;
            record.MedicalNotes = dto.MedicalNotes;
            record.VisitDate = dto.VisitDate;

            await _repository.UpdateAsync(record);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id);

            if (record == null)
                return false;

            await _repository.DeleteAsync(record);

            return true;
        }
    }
}