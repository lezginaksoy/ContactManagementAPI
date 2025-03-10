using ContactManagementAPI.Data.Entities;
using ContactManagementAPI.Data.Repositories;
using ContactManagementAPI.Enums;
using ContactManagementAPI.Models;
using Microsoft.Extensions.Logging;
using System;

namespace ContactManagementAPI.Services
{
    public class ContactsManagmentService : IContactsManagmentService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IFundContactRepository _fundContactRepository;
        private readonly ILogger<IContactsManagmentService> _logger;

        public ContactsManagmentService(IContactRepository contactRepository, IFundContactRepository fundContactRepository, ILogger<IContactsManagmentService> logger)
        {
            _contactRepository = contactRepository;
            _fundContactRepository = fundContactRepository;
            _logger = logger;
        }

        public async Task<ApiResponse> GetAllContactsAsync(int pageNumber = 1, int pageSize = 50)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    _logger.LogWarning("Invalid pagination parameters: Page={PageNumber}, Size={PageSize}", pageNumber, pageSize);
                    return new ApiResponse(ErrorCodes.Invalid_PaginationParams);
                }

                var contacts = await _contactRepository.GetAllPagedAsync(pageNumber, pageSize);
                return new ApiResponse(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetAllContactsAsync");
                return new ApiResponse(ErrorCodes.Unknown_error);
            }
        }

        public async Task<ApiResponse> GetContactByIdAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogWarning("GetContactByIdAsync with invalidId, Id=0");
                    return new ApiResponse(ErrorCodes.Invalid_ContactId);
                }

                var contact = await _contactRepository.GetByIdAsync(id);
                if (contact == null)
                {
                    _logger.LogWarning("GetContactByIdAsync contact Notfound");
                    return new ApiResponse(ErrorCodes.Contact_NotFound);
                }

                return new ApiResponse() { Result = contact };
            }
            catch (Exception ex)
            {
                _logger.LogError("ContactsManagment exception ! " + ex.Message);
                return new ApiResponse(ErrorCodes.Unknown_error);
            }

        }

        public async Task<ApiResponse> AddContactAsync(Contact entity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity.Name))
                {
                    _logger.LogWarning("Contact Name is nullorempty");
                    return new ApiResponse(ErrorCodes.ContactName_IsEmpty);
                }

                var newContactCount = await _contactRepository.AddAsync(entity);

                if (newContactCount <= 0)
                {
                    _logger.LogWarning("added new contact failed!");
                    return new ApiResponse(ErrorCodes.Contact_Creation_Failed);
                }

                return new ApiResponse(ErrorCodes.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContactsManagment exception ! " + ex.Message);
                return new ApiResponse(ErrorCodes.Unknown_error);
            }

        }

        public async Task<ApiResponse> UpdateContactAsync(int id,Contact updatedContact)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogWarning("Invalid contactId , Id=0");
                    return new ApiResponse(ErrorCodes.Invalid_ContactId);
                }

                if (string.IsNullOrWhiteSpace(updatedContact.Name))
                {
                    _logger.LogWarning("Contact Name is nullorempty");
                    return new ApiResponse(ErrorCodes.ContactName_IsEmpty);
                }

                var existingContact = await _contactRepository.GetByIdAsync(id);
                if (existingContact == null)
                {
                    _logger.LogWarning("contact Not found");
                    return new ApiResponse(ErrorCodes.Contact_NotFound);
                }

                existingContact.Name = updatedContact.Name ?? existingContact.Name;
                existingContact.Email = updatedContact.Email;
                existingContact.PhoneNumber = updatedContact.PhoneNumber;
                var updatedContactCount = await _contactRepository.UpdateAsync(existingContact);

                if (updatedContactCount <= 0)
                {
                    _logger.LogWarning("Update contact failed!");
                    return new ApiResponse(ErrorCodes.Contact_Update_Failed);
                }

                return new ApiResponse(ErrorCodes.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContactsManagment exception ! " + ex.Message);
                return new ApiResponse(ErrorCodes.Unknown_error);
            }
        }

        public async Task<ApiResponse> DeleteContactAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogWarning("Invalid contactId , Id=0");
                    return new ApiResponse(ErrorCodes.Invalid_ContactId);
                }

                var existingContact = await _contactRepository.GetByIdAsync(id);
                if (existingContact == null)
                {
                    _logger.LogWarning("contact Not found");
                    return new ApiResponse(ErrorCodes.Contact_NotFound);
                }

                if (await _contactRepository.IsContactAssignedToFundAsync(id))
                {
                    _logger.LogWarning("Cannot delete a contact assigned to a fund.");
                    return new ApiResponse(ErrorCodes.Contact_IsAssigned_ToFund);
                }

                var DeletedContactCount = await _contactRepository.DeleteAsync(existingContact);
                if (DeletedContactCount <= 0)
                {
                    _logger.LogWarning("DeletedContactCount contact failed!");
                    return new ApiResponse(ErrorCodes.Contact_Update_Failed);
                }

                return new ApiResponse(ErrorCodes.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContactsManagment exception ! " + ex.Message);
                return new ApiResponse(ErrorCodes.Unknown_error);
            }

        }

        public async Task<ApiResponse> AssignContactToFundAsync(int fundId, int contactId)
        {
            try
            {
                if (fundId == 0)
                {
                    _logger.LogWarning("Invalid fundId , fundId=0");
                    return new ApiResponse(ErrorCodes.Invalid_FundId);
                }

                if (contactId == 0)
                {
                    _logger.LogWarning("Invalid contactId , contactId=0");
                    return new ApiResponse(ErrorCodes.Invalid_ContactId);
                }


                if (await _fundContactRepository.ExistsAsync(fundId, contactId))
                {
                    _logger.LogWarning("Contact is already assigned to this fund.");
                    return new ApiResponse(ErrorCodes.Contact_Already_Assigned_ThisFund);
                }

                var newContactFund = new FundContact { FundId = fundId, ContactId = contactId };
                var newContactFundCount = await _fundContactRepository.AddAsync(newContactFund);
                if (newContactFundCount <= 0)
                {
                    _logger.LogWarning("Assign fund to contact failed!");
                    return new ApiResponse(ErrorCodes.Assign_Fund_To_Contact_Failed);
                }

                return new ApiResponse(ErrorCodes.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContactsManagment exception ! " + ex.Message);
                return new ApiResponse(ErrorCodes.Unknown_error);
            }

        }

        public async Task<ApiResponse> RemoveContactFromFundAsync(int fundId, int contactId)
        {
            try
            {
                if (fundId == 0)
                {
                    _logger.LogWarning("Invalid fundId , fundId=0");
                    return new ApiResponse(ErrorCodes.Invalid_FundId);
                }

                if (contactId == 0)
                {
                    _logger.LogWarning("Invalid contactId , contactId=0");
                    return new ApiResponse(ErrorCodes.Invalid_ContactId);
                }

                var contactFund = await _fundContactRepository.GetAsync(fundId, contactId);
                if (contactFund == null)
                {
                    _logger.LogWarning("Contact is not assigned to this fund.");
                    return new ApiResponse(ErrorCodes.Contact_Not_Assigned_To_Fund);
                }

                var DeletedContactCount = await _fundContactRepository.RemoveAsync(contactFund);

                if (DeletedContactCount <= 0)
                {
                    _logger.LogWarning("DeletedContactCount contact failed!");
                    return new ApiResponse(ErrorCodes.Remove_ContactFund_Failed);
                }

                return new ApiResponse(ErrorCodes.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContactsManagment exception ! " + ex.Message);
                return new ApiResponse(ErrorCodes.Unknown_error);
            }
        }

        public async Task<ApiResponse> GetContactsByFundAsync(int fundId)
        {
            try
            {
                if (fundId == 0)
                {
                    _logger.LogWarning("Invalid fundId , fundId=0");
                    return new ApiResponse(ErrorCodes.Invalid_FundId);
                }

                var contacts = await _fundContactRepository.GetContactsByFundAsync(fundId);

                return new ApiResponse(ErrorCodes.Success) { Result = contacts };
            }
            catch (Exception ex)
            {
                _logger.LogError("ContactsManagment exception ! " + ex.Message);
                return new ApiResponse(ErrorCodes.Unknown_error);
            }
        }
    }
}
