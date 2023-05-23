using Appointments.Services;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Get([FromQuery] string id)
        {
            _clientService.Get(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _clientService.CreateAsync();

            return Ok();
        }
    }
}
