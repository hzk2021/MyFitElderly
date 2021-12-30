using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDP_Project.Models.Survey
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public string QuestionUUID { get; set; }

        [Required(ErrorMessage ="Please enter the question")]
        public string Text { get; set; }

        public string BelongsToSurveyID { get; set; }
    }
}
