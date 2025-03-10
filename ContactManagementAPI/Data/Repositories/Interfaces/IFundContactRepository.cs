using ContactManagementAPI.Data.Entities;

namespace ContactManagementAPI.Data.Repositories
{
    public interface IFundContactRepository
    {      
        Task<bool> ExistsAsync(int fundId, int contactId);
        Task<int> AddAsync(FundContact fundContact);
        Task<int> RemoveAsync(FundContact fundContact);
        Task<FundContact?> GetAsync(int fundId, int contactId);
        Task<IEnumerable<Contact>> GetContactsByFundAsync(int fundId);
    }
}
