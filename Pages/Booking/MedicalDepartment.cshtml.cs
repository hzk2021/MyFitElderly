using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models.Booking;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Booking
{
    public class MedicalDepartmentModel : PageModel
    {
        private readonly BookingService _svc;
        public List<SpecialistDepartment> AllDepartment { get; set; }
        public MedicalDepartmentModel(BookingService svc)
        {
            _svc = svc;
        }
        public void OnGet()
        {
            AllDepartment = _svc.getDepartmentRecords();
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
