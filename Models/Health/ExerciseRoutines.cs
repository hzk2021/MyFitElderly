using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class ExerciseRoutines
    {
        [Key]
        public int RoutineId { get; set; }

        public int UserId { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        [Required]
        public double Intensity { get; set; }

        [Required]
        public String Days { get; set; }

        public Exercise ExerciseDetails { get; set; }
    }
}
