using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages
{
    public class HealthModel : PageModel
    {
        private readonly HealthService _svc;
        private readonly UserService _userSvc;

        [BindProperty]
        public string[] JournalStatus { get; set; }

        [BindProperty]
        public string[] MedicationStatus { get; set; }

        [BindProperty]
        public string[] ExerciseStatus { get; set; }

        [BindProperty]
        public string[] CaloriesStatus { get; set; }
        
        public HealthModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
            JournalStatus = new string[3];
            MedicationStatus = new string[3];
            ExerciseStatus = new string[3];
            CaloriesStatus = new string[3];
        }
        public void OnGet()
        {
            _userSvc.AIOCheckGuest();

            int userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));

            //Journal Status
            List<Meals> todayMeals = _svc.GetTodayMealAdded(userId);
            if (todayMeals.Count > 0)
            {
                JournalStatus[0] = "<i class=\"fa-solid fa-circle-check\"></i> You have planned/recorded your meals for today";
                JournalStatus[1] = "text-success";
                JournalStatus[2] = "alert-success";
            }
            else
            {
                JournalStatus[0] = "<i class=\"fa-solid fa-triangle-exclamation\"></i> You have yet to add your record for today";
                JournalStatus[1] = "text-warning";
                JournalStatus[2] = "alert-warning";
            }

            //Exercise Status
            List<ExerciseRoutines> myRoutines = _svc.GetRoutine(userId);
            if (myRoutines.Count == 0)
            {
                ExerciseStatus[0] = "<i class=\"fa-solid fa-triangle-exclamation\"></i> You have not set any exercise routine. Recommended to set one";
                ExerciseStatus[1] = "text-danger";
                ExerciseStatus[2] = "alert-danger";
            }
            else if (myRoutines.Any(x => x.Days.Split(",").Contains(DateTime.Now.DayOfWeek.ToString())) )
            {
                ExerciseStatus[0] = "<i class=\"fa-solid fa-triangle-exclamation\"></i> REMINDER: You have a routine today. Click to view your routine";
                ExerciseStatus[1] = "text-warning";
                ExerciseStatus[2] = "alert-warning";
            }
            else
            {
                ExerciseStatus[0] = "<i class=\"fa-solid fa-circle-check\"></i> You do not have any routine today. Remember to do your routines for other days.";
                ExerciseStatus[1] = "text-success";
                ExerciseStatus[2] = "alert-success";
            }

            //Calories Status
            string[] caloriesEvaluation = _svc.EvaluateCalories(userId, 1);
            CaloriesStatus[0] = caloriesEvaluation[0];
            CaloriesStatus[1] = caloriesEvaluation[1];
            CaloriesStatus[2] = caloriesEvaluation[2];
        }
    }
}
