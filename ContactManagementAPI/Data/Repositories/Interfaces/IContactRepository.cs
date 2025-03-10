using ContactManagementAPI.Data.Entities;

namespace ContactManagementAPI.Data.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<Contact?> GetByIdAsync(int id);
        Task<int> AddAsync(Contact contact);
        Task<int> UpdateAsync(Contact contact);
        Task<int> DeleteAsync(Contact contact);
        Task<bool> IsContactAssignedToFundAsync(int contactId);
    }
}
