using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class CaloriesIntakes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public String Day { get; set; }

        public double CaloriesIntake { get; set; }

        public double CaloriesBurned { get; set; }

    }
}
