using BlazorWebassembly_Appointment.Client.Services;
using BlazorWebassembly_Appointment.Server.Data;
using BlazorWebassembly_Appointment.Shared;
using BlazorWebassembly_Appointment.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebassembly_Appointment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DoctorsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDetail>>> GetDoctors()
        {

            return await _context.DoctorDetails.ToListAsync();
        }

        // GET: api/doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDetail>> GetDoctor(int id)
        {
            var doctor = await _context.DoctorDetails.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // POST: api/doctors
        [HttpPost]
        public async Task<ActionResult<DoctorDetail>> PostDoctor(DoctorDetail doctor)
        {
            _context.DoctorDetails.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
        }

        // PUT: api/doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, DoctorDetail doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.DoctorDetails.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.DoctorDetails.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.DoctorDetails.Any(e => e.Id == id);
        }


        // GET: api/doctors/{doctorId}/appointments
        [HttpGet("{doctorId}/appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentModel>>> GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();

            if (appointments == null)
            {
                return NotFound();
            }

            return Ok(appointments);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DoctorDetail>>> SearchDoctors([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Search term is empty.");
            }

            try
            {
                var doctors = await _context.DoctorDetails
                     .Where(d => d.Name.Contains(name))
                     .ToListAsync();

                return Ok(doctors);
            }
            catch (Exception ex)
            {
                // Log the exception (implement a logging mechanism of your choice)
                Console.WriteLine(ex.Message); // or use a logging framework

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetDoctorsList")]
        public async Task<IActionResult> GetDoctorsList()
        {
            try
            {
                // Fetch only users who are in the "Doctor" role
                var doctors = await _userManager.GetUsersInRoleAsync("Doctor");
                var doctorsDto = doctors.Select(d => new DoctorDetail
                {
                    Email = d.Email,
                    Name = d.UserName
                    
                }).ToList();
                return Ok(doctorsDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the doctors.");
                return StatusCode(StatusCodes.Status500InternalServerError, new List<DoctorDetail>());
            }
        }

    }
}

