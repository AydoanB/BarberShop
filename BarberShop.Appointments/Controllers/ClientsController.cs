using BarberShop.Appointments.Models.Users;
using BarberShop.Appointments.Services;
using BarberShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace BarberShop.Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ICurrentUserService _currentUser;

        public ClientsController(IClientService clientService, ICurrentUserService currentUser)
        {
            _clientService = clientService;
            _currentUser = currentUser;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var client = _clientService.GetAsync(id);
            return Ok(client);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _clientService.GetAllAsync();

            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewClientDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _currentUser.UserId;

            try
            {
                await _clientService.CreateAsync(input, userId);
            }
            catch (InvalidOperationException exc)
            {
                return BadRequest(exc.Message);
            }

            return CreatedAtAction(nameof(Get), new { userId = userId}, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _clientService.DeleteAsync(id);

            return NoContent();
        }
    }
}
