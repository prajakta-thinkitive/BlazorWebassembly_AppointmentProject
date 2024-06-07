using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebassembly_Appointment.Shared
{
    public class DoctorDetail
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Treatment { get; set; }

        public string? ProfileImage { get; set; }

        public bool? IsChecked { get; set; } = false;

        //public string UserId { get; set; }
    }
}
