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
        private readonly UserService _userSvc;

        [BindProperty]
        public Food FoodModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public AddFoodModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
        }

        public void OnGet()
        {
            _userSvc.AIOCheckStaff();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(FoodModel.FoodName);
                string response = _svc.AddFood(FoodModel);
                if (response == "true")
                {
                    return RedirectToPage("FoodList");
                }
                else
                {
                    ErrorMsg = response;
                }
            }
            return Page();
        }
    }
}
