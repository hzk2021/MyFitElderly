using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Health
{
    public class DeleteFoodModel : PageModel
    {
        private readonly HealthService _svc;
        public DeleteFoodModel(HealthService svc)
        {
            _svc = svc;
        }
        public IActionResult OnGet(int id)
        {
            _svc.RemoveFood(id);
            return RedirectToPage("FoodList");
        }
    }
}
