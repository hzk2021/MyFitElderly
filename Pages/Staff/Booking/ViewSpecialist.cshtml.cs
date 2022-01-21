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
    public class ViewSpecialistModel : PageModel
    {
        private readonly BookingService _svc;
        public List<Specialist> AllSpecialist { get; set; }
        public ViewSpecialistModel(BookingService svc)
        {
            _svc = svc;
        }
        public void OnGet()
        {
            AllSpecialist = _svc.getSpecialistRecords();
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
