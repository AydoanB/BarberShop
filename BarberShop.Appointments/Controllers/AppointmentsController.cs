using BarberShop.Appointments.Models.Appointment;
using BarberShop.Appointments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var appointment = _appointmentService.GetAsync(id);

            return Ok(appointment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAsync();

            return Ok(appointments);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _appointmentService.DeleteAsync(id);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewAppointmentDto appointment)
        {
            await _appointmentService.CreateAsync(appointment);

            return CreatedAtAction(nameof(Get), new { id = appointment });
        }

    }
}
