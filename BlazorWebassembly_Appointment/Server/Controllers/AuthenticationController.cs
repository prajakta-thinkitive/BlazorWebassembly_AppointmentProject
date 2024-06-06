using BlazorWebassembly_Appointment.Client.Pages.DoctorView;
using BlazorWebassembly_Appointment.Server.Data;
using BlazorWebassembly_Appointment.Shared;
using BlazorWebassembly_Appointment.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebassembly_Appointment.Server.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, [FromQuery] string role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
                if (userExist != null)
                {
                    return BadRequest("User already exists!");
                }

                IdentityUser user = new()
                {
                    Email = registerUser.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerUser.UserName,
                };

                if (await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _userManager.CreateAsync(user, registerUser.Password);
                    if (!result.Succeeded)
                    {
                        return BadRequest("User failed to create!");
                    }

                    await _userManager.AddToRoleAsync(user, role);


                    if (role == "Doctor")
                    {
                        var doctorDetails = new DoctorDetail
                        {
                            Name = registerUser.UserName,
                            Email = registerUser.Email,
                            Specialty = registerUser.Specialty,
                            MobileNumber = registerUser.MobileNumber,
                            Education = registerUser.Education,
                            Treatment = registerUser.Treatment,
                            ProfileImage = registerUser.ProfileImage ?? "default_profile_image.png"
                        };
                        _context.DoctorDetails.Add(doctorDetails);
                        await _context.SaveChangesAsync();
                    }
                    return Ok("User created successfully!");
                }
                else
                {
                    return BadRequest("This role does not exist!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering the user.");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.FindByEmailAsync(loginUser.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid login attempt.");
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, loginUser.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Return some user information or token if required
                    return Ok("Login successful!");
                }
                else
                {
                    return Unauthorized("Invalid login attempt.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging in.");
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Logged out successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging out.");
            }
        }


        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(user);
                if (user == null)
                {
                   return NotFound();
                }

                var model = new CurrentUserDto
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Role = roles?.FirstOrDefault()
                };

                return Ok(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the profile.");
                return StatusCode(StatusCodes.Status500InternalServerError,new CurrentUserDto());
            }
        }

        //[HttpGet("GetDoctorsList")]
        //public async Task<IActionResult> GetDoctorsList()
        //{
        //    try
        //    {
        //        // Fetch only users who are in the "Doctor" role
        //        var doctors = await _userManager.GetUsersInRoleAsync("Doctor");
        //        var doctorsDto = doctors.Select(d => new CurrentUserDto
        //        {
        //            Email = d.Email,
        //            Name = d.UserName,
        //            Role = "Doctor"
        //        }).ToList();
        //        return Ok(doctorsDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, "An error occurred while fetching the doctors.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, new List<CurrentUserDto>());
        //    }
        //}



    }
}

