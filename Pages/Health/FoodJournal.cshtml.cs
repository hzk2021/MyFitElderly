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
        private readonly UserService _userSvc;

        [BindProperty]
        public List<Food> AllFood { get; set; }

        [BindProperty]
        public DateTime Date { get; set; }

        [BindProperty]
        public List<Meals> TodayRecord { get; set; }

        [BindProperty]
        public List<Meals> MealsModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public FoodJournalModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
            MealsModel = new List<Meals>();
            MealsModel.Add(new Meals() { FoodId = 0, MealType = "Breakfast" });
            MealsModel.Add(new Meals() { FoodId = 0, MealType = "Lunch" });
            MealsModel.Add(new Meals() { FoodId = 0, MealType = "Dinner" });
        }
        public void OnGet()
        {
            AllFood = _svc.GetAllFoodRecords();
            var userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
            TodayRecord = _svc.GetTodayRecordAdded(userId);
            Console.WriteLine(TodayRecord.Count);
            foreach (var meal in TodayRecord)
            {
                Console.WriteLine(meal.FoodId);
                Console.WriteLine(meal.MealType);
                Console.WriteLine(meal.UserId);
                Console.WriteLine(meal.FoodDetails.FoodName);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
                // Assign same date and userid for all
                for (var i = 0; i < MealsModel.Count; i++)
                {
                    MealsModel[i].Date = Date;
                    MealsModel[i].UserId = userId;
                }
                string response = _svc.AddMeals(MealsModel);
                if (response != "true")
                    ErrorMsg = response;
            }
            return RedirectToPage("FoodJournal");
        }
    }
}
