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
        private readonly UserService _userSvc;

        public DeleteFoodModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
        }
        public IActionResult OnGet(int id)
        {
            _userSvc.AIOCheckStaff();
            _svc.RemoveFood(id);
            return RedirectToPage("FoodList");
        }
    }
}
