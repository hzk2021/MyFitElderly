using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDP_Project.Models.Survey
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        public bool ViewStatus { get; set; }

        /** Need to have ID attribute in User model**/
        //[Required]
        //[ForeignKey("CreatedByStaffID")]
        //public User CreatedByStaffID { get; set; }

        // Alternative
        [Required]
        public int CreatedByStaffID { get; set; }

    }
}
