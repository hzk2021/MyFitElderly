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
    public class UpdateSlotModel : PageModel
    {
        private readonly BookingService _svc;

        public SpecialistSlot SlotModel { get; set; }
        public UpdateSlotModel(BookingService svc)
        {
            _svc = svc;
        }
        public IActionResult OnGet(int id, int specid)
        {
            
            _svc.UpdateSlot(id , specid);
            return RedirectToPage("SlotBooked");
        }

    }
}
