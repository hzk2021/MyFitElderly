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
    public class EditExerciseModel : PageModel
    {
        private readonly HealthService _svc;
        private readonly UserService _userSvc;

        [BindProperty]
        public Exercise ExerciseModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public EditExerciseModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
        }

        public void OnGet(int exerciseId)
        {
            _userSvc.AIOCheckStaff();
            ExerciseModel = _svc.GetExerciseById(exerciseId);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(ExerciseModel.ExerciseName);
                string response = _svc.UpdateExercise(ExerciseModel);
                if (response == "true")
                {
                    return RedirectToPage("ExerciseList");
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
