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
        public bool RecordExists { get; set; }

        [BindProperty]
        public List<Meals> MealsModel { get; set; }

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public bool UpdateOperation { get; set; }

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
            _userSvc.AIOCheckGuest();

            AllFood = _svc.GetAllFoodRecords();
            UserId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
            
            TodayRecord = _svc.GetTodayMealAdded(UserId);
            RecordExists = TodayRecord.Count > 0;
            if (RecordExists)
                Date = TodayRecord[0].Date;
            else
                Date = DateTime.Now;

            UpdateOperation = RecordExists;
           
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState Valid");
                int userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
                // Assign same date and userid for all
                for (var i = 0; i < MealsModel.Count; i++)
                {
                    MealsModel[i].Date = Date;
                    MealsModel[i].UserId = userId;
                }

                string response1;
                if (UpdateOperation)
                {
                    Console.WriteLine("Adding meal");
                    response1 = _svc.AddMeals(MealsModel);
                }
                else
                {
                    Console.WriteLine("Updating meal");
                    response1 = _svc.UpdateMeals(MealsModel, userId);
                }

                string response2 = _svc.CalculateCalories(userId);

                if (response1 != "true" || response2 != "true")
                    ErrorMsg = response1;
            }
            else
            {
                return Page();
            }

            return RedirectToPage("FoodJournal");
        }
    }
}
