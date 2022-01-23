using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models.Booking
{
    public class SpecialistDepartment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a department.")]
        public String Department { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        public Decimal Price { get; set; }
    }
}
