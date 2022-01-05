using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDP_Project.Models.Survey
{
    public class QuestionOption
    {
        [Key]
        public int Id { get; set; }

        public string OptionUUID { get; set; }

        [Required(ErrorMessage = "Please do not leave any blank(s) in option field")]
        public string Text { get; set; }

        public string BelongsToQuestionID { get; set; }
    }
}
