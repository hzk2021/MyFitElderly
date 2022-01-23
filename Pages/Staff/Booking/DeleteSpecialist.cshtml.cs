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
    public class DeleteSpecialistModel : PageModel
    {
        private readonly BookingService _svc;
        [BindProperty]
        public Specialist SpecialistModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public DeleteSpecialistModel(BookingService svc)
        {
            _svc = svc;
        }
        public IActionResult OnGet(int id)
        {
            _svc.DeleteSpecialist(id);
            return RedirectToPage("ViewSpecialist");
        }
    }
}
