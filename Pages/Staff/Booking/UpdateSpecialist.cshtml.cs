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
    public class UpdateSpecialistModel : PageModel
    {
        private readonly BookingService _svc;
        [BindProperty]
        public Specialist SpecialistModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public UpdateSpecialistModel(BookingService svc)
        {
            _svc = svc;
        }

        public void OnGet(int id)
        {
            SpecialistModel = _svc.GetSpecialistById(id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string response = _svc.UpdateSpecialist(SpecialistModel);
                if (response == "True")
                {
                    return RedirectToPage("ViewSpecialist");
                }
            }
            return Page();
        }
    }
}
