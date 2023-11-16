using ContactManager.Business.DTOs.Contact;
using ContactManager.Business.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var contacts = await _contactService.GetAllAsync();

            return Ok(contacts);
        }

        [HttpGet("getById/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var contact = await _contactService.GetByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Attempting to get Contact by Id {id}: {@contact}", id, contact);

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ContactForCreationDto contactForCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _contactService.CreateAsync(contactForCreationDto);

            _logger.LogInformation("New Contact created: {@contactForCreationDto}", contactForCreationDto);

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] ContactForUpdateDto contactForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _contactService.UpdateAsync(contactForUpdateDto);

            _logger.LogInformation("Contact data updated to: {@contactForUpdateDto}", contactForUpdateDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _contactService.DeleteAsync(id);

            _logger.LogInformation("Contact deleted by Id {id}", id);

            return NoContent();
        }
    }
}