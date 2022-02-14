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
    public class MyExerciseRoutineModel : PageModel
    {
        private readonly HealthService _svc;
        private readonly UserService _userSvc;

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public List<string> Days { get; set; }

        [BindProperty]
        public List<double> Calories { get; set; }

        [BindProperty]
        public List<Exercise> AllExercises { get; set; }

        [BindProperty]
        public List<ExerciseRoutines> Routines { get; set; }

        [BindProperty]
        public bool RoutinesExist { get; set; }

        [BindProperty]
        public List<List<string>> GroupedDays { get; set; }

        [BindProperty]
        public List<int> GroupedExerciseCount { get; set; }

        public MyExerciseRoutineModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
            Days = new List<string>();
            Calories = new List<double>();
            GroupedDays = new List<List<string>>();
            GroupedExerciseCount = new List<int>();
        }

        public void OnGet()
        {
            _userSvc.AIOCheckGuest();

            UserId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
            AllExercises = _svc.GetAllExerciseRecords();
            Routines = _svc.GetRoutine(UserId);

            if (Routines.Count == 0)
            {
                RoutinesExist = false;
                Routines.Add(new ExerciseRoutines() { });
                GroupedDays = new List<List<string>>() { Enumerable.Repeat("false", 8).ToList() };
                GroupedExerciseCount = new List<int>() { 0 };
            } 
            else
            {
                RoutinesExist = true;
                int daysCount = 0;

                List<string> listDays = _svc.GetUniqueRoutineDays(UserId);
                List<int> listCount = _svc.GetUniqueDaysCount(UserId);

                foreach (var routine in Routines)
                {
                    daysCount++;
                    if (!Days.Contains(routine.Days))
                    {
                        Days.Add(routine.Days);
                        Calories.Add(routine.Intensity * routine.ExerciseDetails.CaloriesBurnPerUnit);
                    }
                    else
                    {
                        Calories[Days.IndexOf(routine.Days)] += routine.Intensity * routine.ExerciseDetails.CaloriesBurnPerUnit;
                    }
                }

                foreach (var dayStr in listDays)
                {
                    Console.WriteLine(dayStr);
                    string[] boolDays = Enumerable.Repeat("false", 8).ToArray();
                    foreach (var day in dayStr.Split(","))
                    {
                        switch (day)
                        {
                            case "Monday":
                                boolDays[(int)EnumDays.Monday] = "true";
                                break;
                            case "Tuesday":
                                boolDays[(int)EnumDays.Tuesday] = "true";
                                break;
                            case "Wednesday":
                                boolDays[(int)EnumDays.Wednesday] = "true";
                                break;
                            case "Thursday":
                                boolDays[(int)EnumDays.Thursday] = "true";
                                break;
                            case "Friday":
                                boolDays[(int)EnumDays.Friday] = "true";
                                break;
                            case "Saturday":
                                boolDays[(int)EnumDays.Saturday] = "true";
                                break;
                            case "Sunday":
                                boolDays[(int)EnumDays.Sunday] = "true";
                                break;
                        }
                    }

                    GroupedDays.Add(boolDays.ToList());
                }

                GroupedExerciseCount = listCount;
            }
            
        }

        enum EnumDays
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        public IActionResult OnPost()
        {
            int userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));

            // Assign the days for each routine row
            int scannedRows = 0;
            for (var k=0; k < GroupedDays.Count; k++)
            {
                List<string> selectedDaysList = new List<string>();
                for (var i=0; i < GroupedDays[k].Count; i++)
                {
                    if (bool.Parse(GroupedDays[k][i]))
                    {
                        selectedDaysList.Add(((EnumDays)i).ToString());
                    }
                }

                string selectedDaysString = string.Join(",", selectedDaysList);
                for (var j = 0; j < GroupedExerciseCount[k]; j++)
                {
                    Console.WriteLine(scannedRows + j);
                    Routines[scannedRows + j].Days = selectedDaysString;
                    Routines[scannedRows + j].UserId = userId;
                }
                Console.WriteLine("Next");
                scannedRows += GroupedExerciseCount[k];
            }

            _svc.UpdateRoutines(Routines, userId);

            _svc.CalculateCalories(userId);

            return RedirectToPage("");
        }
    }
}
