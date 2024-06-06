using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebassembly_Appointment.Shared
{
    public class AppointmentModel
    {
        [Key]
        public int Id { get; set; }

        public int DoctorId { get; set; }
        public DateTime SelectedDate { get; set; }
        public string Slot { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Description { get; set; }
    }
}
