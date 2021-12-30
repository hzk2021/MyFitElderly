using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages
{
    public class AddFoodModel : PageModel
    {
        private readonly HealthService _svc;
        [BindProperty]
        public Food FoodModel { get; set; }

        public AddFoodModel(HealthService svc)
        {
            _svc = svc;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddFood(FoodModel))
                {
                    return RedirectToPage("Privacy");
                }
            }
            return Page();
        }
    }
}
