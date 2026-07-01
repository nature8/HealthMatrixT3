using BillingService.DTOs;
using BillingService.Models;

namespace BillingService.Services
{
    public interface IBillService
    {
        Task CreateAsync(CreateBillDto dto);

        Task<List<Bill>> GetAllAsync();

        Task<Bill?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, CreateBillDto dto);

        Task<bool> DeleteAsync(int id);
    }
}