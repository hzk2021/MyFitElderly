using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages
{
    public class FoodListModel : PageModel
    {
        private readonly HealthService _svc;
        public List<Food> AllFood { get; set; }
        public int PageNo { get; set; }
        public string Category { get; set; }
        public string Search { get; set; }
        public FoodListModel(HealthService svc)
        {
            _svc = svc;
            PageNo = 1;
            Category = "All";
        }
        public void OnGet()
        {
            AllFood = _svc.GetFoodRecords(PageNo, Category, Search);
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
