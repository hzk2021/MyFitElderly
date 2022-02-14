using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        public String ExerciseName { get; set; }

        [Required]
        public String Measurement { get; set; }

        [Required]
        public double CaloriesBurnPerUnit { get; set; }
    }
}
