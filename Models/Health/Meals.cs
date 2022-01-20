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
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public String MealType { get; set; }

        [Required]
        public int FoodId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public Food FoodDetails { get; set; }
    }
}
