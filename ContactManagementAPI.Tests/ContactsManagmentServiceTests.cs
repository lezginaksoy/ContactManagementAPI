using ContactManagementAPI.Data.Entities;
using ContactManagementAPI.Data.Repositories;
using ContactManagementAPI.Enums;
using ContactManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementAPI.Tests
{
    public class ContactsManagmentServiceTests
    {
        private readonly Mock<IContactRepository> _mockContactRepo;
        private readonly Mock<IFundContactRepository> _mockFundRepo;
        private readonly ContactsManagmentService _managmentService;
        private readonly ILogger<ContactsManagmentService> _logger;

        public ContactsManagmentServiceTests()
        {
            _mockContactRepo = new Mock<IContactRepository>();
            _mockFundRepo = new Mock<IFundContactRepository>();
            _logger = new LoggerFactory().CreateLogger<ContactsManagmentService>();
            _managmentService = new ContactsManagmentService(_mockContactRepo.Object, _mockFundRepo.Object, _logger);
        }

        [Fact]
        public async Task GetAllContactsAsync_ShouldReturnContacts()
        {
            // Arrange
            var contacts = new List<Contact> {
                new Contact { Id = 1, Name = "lzgn Aksoy" },
                new Contact { Id = 1, Name = "Tim blair" }
            };
            _mockContactRepo.Setup(r => r.GetAllPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
                     .ReturnsAsync(contacts);

            // Act
            var response = await _managmentService.GetAllContactsAsync(1, 10);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal((int)ErrorCodes.Success, response.Code);
            Assert.NotNull(response.Result);
        }

        [Fact]
        public async Task GetContactByIdAsync_ShouldReturnContact_WhenContactExists()
        {
            // Arrange
            var contact = new Contact { Id = 1, Name = "lazgin aksoy" };
            _mockContactRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(contact);

            // Act
            var response = await _managmentService.GetContactByIdAsync(1);

            // Assert
            Assert.Equal((int)ErrorCodes.Success, response.Code);
            Assert.NotNull(response.Result);
        }

        [Fact]
        public async Task GetContactByIdAsync_ShouldReturnNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            _mockContactRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Contact)null);

            // Act
            var response = await _managmentService.GetContactByIdAsync(1);

            // Assert
            Assert.Equal((int)ErrorCodes.Contact_NotFound, response.Code);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task AddContactAsync_ShouldReturnSuccess_WhenContactCreated()
        {
            // Arrange
            var contact = new Contact { Name = "lazgin Aksoy" };
            _mockContactRepo.Setup(r => r.AddAsync(contact)).ReturnsAsync(1);

            // Act
            var response = await _managmentService.AddContactAsync(contact);

            // Assert
            Assert.Equal((int)ErrorCodes.Success, response.Code);
        }

        [Fact]
        public async Task AddContactAsync_ShouldReturnError_WhenContactNameIsEmpty()
        {
            // Arrange
            var contact = new Contact { Name = "" };

            // Act
            var response = await _managmentService.AddContactAsync(contact);

            // Assert      
            Assert.Equal((int)ErrorCodes.ContactName_IsEmpty, response.Code);
        }

        [Fact]
        public async Task UpdateContactAsync_ShouldReturnSuccess_WhenUpdatedSuccessfully()
        {
            // Arrange
            var contact = new Contact { Id = 1, Name = "lazgin Aksoy(Updated)", Email = "lazginaksoytest@gmail.com" };
            _mockContactRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(contact);
            _mockContactRepo.Setup(r => r.UpdateAsync(It.IsAny<Contact>())).ReturnsAsync(1);

            // Act
            var response = await _managmentService.UpdateContactAsync(1, contact);

            // Assert         
            Assert.Equal((int)ErrorCodes.Success, response.Code);
        }

        [Fact]
        public async Task UpdateContactAsync_ShouldReturnError_WhenContactNotFound()
        {
            // Arrange
            _mockContactRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Contact)null);

            // Act
            var response = await _managmentService.UpdateContactAsync(1, new Contact { Id = 1, Name = "New Jack" });

            // Assert         
            Assert.Equal((int)ErrorCodes.Contact_NotFound, response.Code);
        }

        [Fact]
        public async Task DeleteContactAsync_ShouldReturnSuccess_WhenDeletedSuccessfully()
        {
            // Arrange
            var contact = new Contact { Id = 1, Name = "John Doe" };
            _mockContactRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(contact);
            _mockContactRepo.Setup(r => r.DeleteAsync(contact)).ReturnsAsync(1);

            // Act
            var response = await _managmentService.DeleteContactAsync(1);

            // Assert
            Assert.Equal((int)ErrorCodes.Success, response.Code);
        }

        [Fact]
        public async Task DeleteContactAsync_ShouldReturnError_WhenContactNotFound()
        {
            // Arrange
            _mockContactRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Contact)null);

            // Act
            var response = await _managmentService.DeleteContactAsync(1);

            // Assert
            Assert.Equal((int)ErrorCodes.Contact_NotFound, response.Code);
        }

        [Fact]
        public async Task AssignContactToFundAsync_ShouldReturnSuccess_WhenContactIsAssigned()
        {
            // Arrange
            _mockFundRepo.Setup(r => r.ExistsAsync(1, 1)).ReturnsAsync(false);
            _mockFundRepo.Setup(r => r.AddAsync(It.IsAny<FundContact>())).ReturnsAsync(1);

            // Act
            var response = await _managmentService.AssignContactToFundAsync(1, 1);

            // Assert
            Assert.Equal((int)ErrorCodes.Success, response.Code);
        }

        [Fact]
        public async Task AssignContactToFundAsync_ShouldReturnError_WhenContactAlreadyAssigned()
        {
            // Arrange
            _mockFundRepo.Setup(r => r.ExistsAsync(1, 1)).ReturnsAsync(true);

            // Act
            var response = await _managmentService.AssignContactToFundAsync(1, 1);

            // Assert          
            Assert.Equal((int)ErrorCodes.Contact_Already_Assigned_ThisFund, response.Code);
        }

        [Fact]
        public async Task AssignContactToFundAsync_ShouldReturnError_WhenFundIdIsInvalid()
        {
            // Act
            var response = await _managmentService.AssignContactToFundAsync(0, 1);

            // Assert           
            Assert.Equal((int)ErrorCodes.Invalid_FundId, response.Code);
        }

        [Fact]
        public async Task RemoveContactFromFundAsync_ShouldReturnSuccess_WhenContactIsRemoved()
        {
            // Arrange
            var fundContact = new FundContact { FundId = 1, ContactId = 1 };
            _mockFundRepo.Setup(r => r.GetAsync(1, 1)).ReturnsAsync(fundContact);
            _mockFundRepo.Setup(r => r.RemoveAsync(fundContact)).ReturnsAsync(1);

            // Act
            var response = await _managmentService.RemoveContactFromFundAsync(1, 1);

            // Assert
            Assert.Equal((int)ErrorCodes.Success, response.Code);
        }

        [Fact]
        public async Task RemoveContactFromFundAsync_ShouldReturnError_WhenContactNotAssignedToFund()
        {
            // Arrange
            _mockFundRepo.Setup(r => r.GetAsync(1, 1)).ReturnsAsync((FundContact)null);

            // Act
            var response = await _managmentService.RemoveContactFromFundAsync(1, 1);

            // Assert
            Assert.Equal((int)ErrorCodes.Contact_Not_Assigned_To_Fund, response.Code);
        }

        [Fact]
        public async Task RemoveContactFromFundAsync_ShouldReturnError_WhenFundIdIsInvalid()
        {
            // Act
            var response = await _managmentService.RemoveContactFromFundAsync(0, 1);

            // Assert         
            Assert.Equal((int)ErrorCodes.Invalid_FundId, response.Code);
        }

        [Fact]
        public async Task GetContactsByFundAsync_ShouldReturnContacts_WhenFundHasContacts()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Lazgin" },
                new Contact { Id = 2, Name = "Hnn" }
            };
            _mockFundRepo.Setup(r => r.GetContactsByFundAsync(1)).ReturnsAsync(contacts);

            // Act
            var response = await _managmentService.GetContactsByFundAsync(1);

            // Assert
            Assert.Equal((int)ErrorCodes.Success, response.Code);
            Assert.NotNull(response.Result);
        }
                
        [Fact]
        public async Task GetContactsByFundAsync_ShouldReturnError_WhenFundIdIsInvalid()
        {
            // Act
            var response = await _managmentService.GetContactsByFundAsync(0);

            // Assert           
            Assert.Equal((int)ErrorCodes.Invalid_FundId, response.Code);
        }
    }
}
