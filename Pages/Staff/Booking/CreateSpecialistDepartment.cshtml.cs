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
    public class CreateSpecialistDepartmentModel : PageModel
    {
        private readonly BookingService _svc;
        [BindProperty]
        public SpecialistDepartment SpecialistDepartmentModel { get; set; }

        public CreateSpecialistDepartmentModel(BookingService svc)
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
                Console.WriteLine(SpecialistDepartmentModel.Department);
                string response = _svc.CreateSpecialistDepartment(SpecialistDepartmentModel);
                if (response == "True")
                {
                    return RedirectToPage("/");
                }
            }
            return Page();
        }
    }
}
