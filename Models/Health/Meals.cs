using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class Meals
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public String MealType { get; set; }

        [Required]
        public int FoodId { get; set; }

        [Required]
        public int Date { get; set; }
    }
}
