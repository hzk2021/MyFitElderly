using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models.Booking;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Booking
{
    public class CreateSpecialistModel : PageModel
    {
        private readonly BookingService _svc;
        [BindProperty]
        public Specialist SpecialistModel { get; set; }

        public CreateSpecialistModel(BookingService svc)
        {
            _svc = svc;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(SpecialistModel.Department);
                string response = _svc.CreateSpecialist(SpecialistModel);
                if (response == "True")
                {   
                    return RedirectToPage("ViewSpecialist");
                }
                Console.WriteLine(response);
            }
            return Page();
        }
    }
}