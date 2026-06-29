using BillingService.Models;

namespace BillingService.Repositories
{
    public interface IBillRepository
    {
        Task AddAsync(Bill bill);

        Task<List<Bill>> GetAllAsync();

        Task<Bill?> GetByIdAsync(int id);

        Task UpdateAsync(Bill bill);

        Task DeleteAsync(Bill bill);
    }
}