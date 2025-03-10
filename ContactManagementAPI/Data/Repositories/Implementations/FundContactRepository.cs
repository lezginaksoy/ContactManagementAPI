using ContactManagementAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPI.Data.Repositories.Implementations
{
    public class FundContactRepository : IFundContactRepository
    {
        private readonly AppDbContext _context;

        public FundContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int fundId, int contactId)
        {
            return await _context.FundContacts.AnyAsync(fc => fc.FundId == fundId && fc.ContactId == contactId);
        }

        public async Task<int> AddAsync(FundContact fundContact)
        {
            _context.FundContacts.Add(fundContact);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(FundContact fundContact)
        {
            _context.FundContacts.Remove(fundContact);
            return await _context.SaveChangesAsync();
        }

        public async Task<FundContact?> GetAsync(int fundId, int contactId)
        {
            return await _context.FundContacts.FirstOrDefaultAsync(fc => fc.FundId == fundId && fc.ContactId == contactId);
        }

        public async Task<IEnumerable<Contact>> GetContactsByFundAsync(int fundId)
        {
            return await _context.FundContacts
                .Where(fc => fc.FundId == fundId)
                .Select(fc => fc.Contact)
                .ToListAsync();
        }
   
    }
}
