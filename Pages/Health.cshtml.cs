using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages
{
    public class HealthModel : PageModel
    {
        public string JournalStatus { get; set; }
        public string MedicationStatus { get; set; }
        public string ExerciseStatus { get; set; }
        public string CaloriesStatus { get; set; }
        public void OnGet()
        {

        }
    }
}
