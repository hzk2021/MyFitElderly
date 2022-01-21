using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models.Booking
{
    public class Specialist
    {
        internal int id;

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Please enter a department.")]
        public String Department { get; set; }

        [Required(ErrorMessage = "Please enter a profession.")]
        public String Profession { get; set; }

        [Required(ErrorMessage = "Please enter a hospital.")]
        public String Hospital { get; set; }

        [Required(ErrorMessage = "Please enter a expertise.")]
        public String Expertise { get; set; }
    }
}
