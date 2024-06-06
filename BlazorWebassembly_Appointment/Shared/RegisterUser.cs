using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebassembly_Appointment.Shared
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        public string? Specialty { get; set; }
        public string? MobileNumber { get; set; }
        public string? Education { get; set; }
        public string? Treatment { get; set; }
        public string? ProfileImage { get; set; }
    }
}
