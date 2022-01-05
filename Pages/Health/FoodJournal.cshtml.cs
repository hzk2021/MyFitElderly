using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Health
{
    public class FoodJournalModel : PageModel
    {
        private readonly HealthService _svc;
        [BindProperty]
        public List<Food> AllFood { get; set; }
        public FoodJournalModel(HealthService svc)
        {
            _svc = svc;
        }
        public void OnGet()
        {
            AllFood = _svc.GetAllFoodRecords();
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
