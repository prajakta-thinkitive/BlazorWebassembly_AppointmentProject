using BlazorWebassembly_Appointment.Server.Data;
using BlazorWebassembly_Appointment.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebassembly_Appointment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostAppointment(AppointmentModel appointment)
        {
            try
            {
                if (appointment == null)
                {
                    return BadRequest("Appointment is null.");
                }

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");

                // Return a generic error message.
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the appointment.");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AppointmentModel>>> GetAllAppointments()
        {
            try
            {
                var appointments = await _context.Appointments.ToListAsync();

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching appointments: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching appointments.");
            }
        }

        [HttpGet("{doctorId}/{date}")]
        public async Task<IActionResult> GetBookedSlots(int doctorId, DateTime date)
        {
            var bookedSlots = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.SelectedDate.Date == date.Date)
                .Select(a => a.Slot)
                .ToListAsync();

            return Ok(bookedSlots);
        }
    }
}
