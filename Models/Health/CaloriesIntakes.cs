using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class CaloriesIntakes
    {
        [Required, Key]
        public String Date { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public String Day { get; set; }

        public int CaloriesIntake { get; set; }
    }
}
