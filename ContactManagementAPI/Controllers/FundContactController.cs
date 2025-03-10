using ContactManagementAPI.Data.Repositories;
using ContactManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementAPI.Controllers
{
    [Route("api/Funds")]
    [ApiController]
    public class FundContactController : ControllerBase
    {
        private readonly IContactsManagmentService _fundService;

        public FundContactController(IContactsManagmentService fundService)
        {
            _fundService = fundService;
        }

        [HttpPost("{fundId}/contacts/{contactId}")]
        public async Task<IActionResult> AssignContact(int fundId, int contactId)
        {
            var result = await _fundService.AssignContactToFundAsync(fundId, contactId);
            return Ok(result);
        }

        [HttpDelete("{fundId}/contacts/{contactId}")]
        public async Task<IActionResult> RemoveContact(int fundId, int contactId)
        {
            var result = await _fundService.RemoveContactFromFundAsync(fundId, contactId);

            return Ok(result);
        }

        [HttpGet("{fundId}/contacts")]
        public async Task<IActionResult> GetContactsByFund(int fundId)
        {
            var contacts = await _fundService.GetContactsByFundAsync(fundId);
            return Ok(contacts);
        }
    }
}
