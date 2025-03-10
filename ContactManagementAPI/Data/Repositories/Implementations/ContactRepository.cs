using ContactManagementAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ContactManagementAPI.Data.Repositories.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<int> AddAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            return await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Contact>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Contacts.AsQueryable();
            var contacts = await query
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return contacts;
        }

        public async Task<int> UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Contact contact)
        {
            _context.Contacts.Remove(contact);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> IsContactAssignedToFundAsync(int contactId)
        {
            return await _context.FundContacts.AnyAsync(fc => fc.ContactId == contactId);
        }

    }
}
