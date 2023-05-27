using Appointments.Models;
using Appointments.Services;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var appointment = _appointmentService.Get(id);

            return Ok(appointment);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _appointmentService.DeleteAsync(id);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Appointment appointment)
        {
            await _appointmentService.CreateAsync(appointment);

            return CreatedAtAction(nameof(Get), new { id = appointment.Id });
        }

    }
}
