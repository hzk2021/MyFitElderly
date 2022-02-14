using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        public String FoodName { get; set; }

        [Required]
        public String Category { get; set; }

        [Required]
        public double Calories { get; set; }
    }
}
