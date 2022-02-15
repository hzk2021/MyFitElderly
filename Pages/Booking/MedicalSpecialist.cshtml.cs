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
    public class MedicalSpecialistModel : PageModel
    {
        private readonly BookingService _svc;
        public List<Specialist> AllSpecialist { get; set; }
        public SpecialistDepartment OneDepartment { get; set; }
        public MedicalSpecialistModel(BookingService svc)
        {
            _svc = svc;
        }
        public void OnGet(int id)
        {
            AllSpecialist = _svc.getSpecialistRecords();
            OneDepartment = _svc.GetDepartmentById(id);
            
            int spec = id;
        }
        public IActionResult OnPost()
        {
            return Page();
        }

        public void selectSpecialist_btn(Object sender, EventArgs e)
        {

        }
    }
}
