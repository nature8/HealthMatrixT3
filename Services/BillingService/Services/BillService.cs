using BillingService.DTOs;
using BillingService.Models;
using BillingService.Repositories;

namespace BillingService.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _repository;

        public BillService(IBillRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateBillDto dto)
        {
            var bill = new Bill
            {
                PatientId = dto.PatientId,
                AppointmentId = dto.AppointmentId,
                Amount = dto.Amount,
                Status = dto.Status,
                CreatedAt = DateTime.Now
            };

            await _repository.AddAsync(bill);
        }

        public async Task<List<Bill>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Bill?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, CreateBillDto dto)
        {
            var bill = await _repository.GetByIdAsync(id);

            if (bill == null)
                return false;

            bill.PatientId = dto.PatientId;
            bill.AppointmentId = dto.AppointmentId;
            bill.Amount = dto.Amount;
            bill.Status = dto.Status;

            await _repository.UpdateAsync(bill);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bill = await _repository.GetByIdAsync(id);

            if (bill == null)
                return false;

            await _repository.DeleteAsync(bill);

            return true;
        }
    }
}