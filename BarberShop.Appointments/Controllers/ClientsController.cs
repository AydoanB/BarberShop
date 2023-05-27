using Appointments.Models.DTOs;
using Appointments.Services;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var client = _clientService.Get(id);
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewClientDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _clientService.CreateAsync(client);

            return CreatedAtAction(nameof(Get), new { id = id}, client);
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
