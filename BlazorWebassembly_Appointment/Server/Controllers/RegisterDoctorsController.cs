//using BlazorWebassembly_Appointment.Server.Data;
//using BlazorWebassembly_Appointment.Shared;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Numerics;

//namespace BlazorWebassembly_Appointment.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RegisterDoctorsController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public RegisterDoctorsController(ApplicationDbContext context)
//        {
//            _context = context;     
//        }


//        [HttpGet("GetAllDoctors")]
//        public async Task<ActionResult<IEnumerable<RegisterDoctors>>> GetAllDoctors()
//        {
//            var doctors = await _context.RegisterDoctors.ToListAsync();
//            return Ok(doctors);
//        }
//    }
//}
