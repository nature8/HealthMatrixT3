using BillingService.Data;
using BillingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingService.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly BillingDbContext _context;

        public BillRepository(BillingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bill>> GetAllAsync()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<Bill?> GetByIdAsync(int id)
        {
            return await _context.Bills
                .FirstOrDefaultAsync(x => x.BillId == id);
        }

        public async Task UpdateAsync(Bill bill)
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Bill bill)
        {
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
        }
    }
}