using ContactManagementAPI.Data.Entities;
using ContactManagementAPI.Models;

namespace ContactManagementAPI.Services
{
    public interface IContactsManagmentService
    {
        Task<ApiResponse> GetAllContactsAsync(int pageNumber, int pageSize);
        Task<ApiResponse> GetContactByIdAsync(int id);
        Task<ApiResponse> AddContactAsync(Contact contact);
        Task<ApiResponse> UpdateContactAsync(int id,Contact contact);
        Task<ApiResponse> DeleteContactAsync(int id);
        Task<ApiResponse> AssignContactToFundAsync(int fundId, int contactId);
        Task<ApiResponse> RemoveContactFromFundAsync(int fundId, int contactId);
        Task<ApiResponse> GetContactsByFundAsync(int fundId);
    }
}
