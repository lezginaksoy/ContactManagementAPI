using ContactManagementAPI.Data.Entities;
using ContactManagementAPI.Data.Repositories;
using ContactManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementAPI.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactsManagmentService _contactService;

        public ContactController(IContactsManagmentService service)
        {
            _contactService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _contactService.GetAllContactsAsync(pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var res = await _contactService.GetContactByIdAsync(id);            
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            var res = await _contactService.AddContactAsync(contact);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact updatedContact)
        {
            var res = await _contactService.UpdateContactAsync(id,updatedContact);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var res = await _contactService.DeleteContactAsync(id);
            return Ok(res);
        }
    }
}
