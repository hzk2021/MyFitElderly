using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models.Booking;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Booking
{
    public class MedicalSlotModel : PageModel
    {
        private readonly BookingService _svc;
        public List<SpecialistSlot> SpecialistSlotModel{ get; set; }

        public MedicalSlotModel(BookingService svc)
        {
            _svc = svc;
        }
        public void OnGet(int id)
        {
            SpecialistSlotModel = _svc.GetSlotBySpecId(id);
        }

        public void post()
        {

        }
    }
}
