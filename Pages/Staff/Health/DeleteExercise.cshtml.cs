using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Health
{
    public class DeleteExerciseModel : PageModel
    {
        private readonly HealthService _svc;
        private readonly UserService _userSvc;
        public DeleteExerciseModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
        }
        public IActionResult OnGet(int exerciseId)
        {
            _userSvc.AIOCheckStaff();
            _svc.RemoveExercise(exerciseId);
            return RedirectToPage("ExerciseList");
        }
    }
}
