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
        public DeleteExerciseModel(HealthService svc)
        {
            _svc = svc;
        }
        public IActionResult OnGet(int exerciseId)
        {
            _svc.RemoveExercise(exerciseId);
            return RedirectToPage("ExerciseList");
        }
    }
}
