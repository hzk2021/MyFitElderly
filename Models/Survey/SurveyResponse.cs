using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models.Survey
{
    public class SurveyResponse
    {
        [Key]
        public int Id { get; set; }

        public string SurveyResponseUUID { get; set; }

        public string Question_Text { get; set; }

        [Required(ErrorMessage = "Please choose a response")]
        public string Response_Text { get; set; }
        public string ReferenceToSurveyID { get; set; }
        public int SubmittedByCustomerID { get; set; }

    }
}
