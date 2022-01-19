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

        public string SurveyUUID { get; set; }

        [Required(ErrorMessage ="Please enter a category")]
        public string Category { get; set; }

        [Required(ErrorMessage ="Please enter a title")]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string ViewStatus { get; set; } = "Hidden";

        /** Need to have ID attribute in User model**/
        //[ForeignKey("CreatedByStaffID")]
        //public User CreatedByStaffID { get; set; }

        // Alternative
        public int CreatedByStaffID { get; set; }

    }
}
