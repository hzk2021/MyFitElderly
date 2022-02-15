using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models.Booking
{
    public class SpecialistSlot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required] 
        public int SpecId { get; set; }
        public bool Slot1 { get; set; }
        public bool Slot2 { get; set; }
        public bool Slot3 { get; set; }
        public bool Slot4 { get; set; }
        public bool Slot5 { get; set; }
        public bool Slot6 { get; set; }
        public bool Slot7 { get; set; }
        public bool Slot8 { get; set; }
    }
}
