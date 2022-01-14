using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Survey
{
    public class CreateSurveyModel : PageModel
    {
        private SurveyService _srv;

        [BindProperty]
        public Models.Survey.Survey newSurvey { get; set; }

        [BindProperty]
        public List<Models.Survey.Question> AllQuestionList { get; set; }

        [BindProperty]
        public List<Models.Survey.QuestionOption> qnsOptions { get; set; }

        public CreateSurveyModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }

        public string surveyUUID = Guid.NewGuid().ToString();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Models.Survey.Survey svy = new Models.Survey.Survey()
                {
                    SurveyUUID = newSurvey.SurveyUUID,
                    Category = newSurvey.Category,
                    Title = newSurvey.Title,
                    Description = newSurvey.Description,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    ViewStatus = false,
                    CreatedByStaffID = 7 // Temp | change later
                };
                _srv.AddSurvey(svy);

                for (int i = 0; i < AllQuestionList.Count; i++)
                {
                    _srv.AddQuestionToSurvey(AllQuestionList[i].QuestionUUID, AllQuestionList[i].Text, AllQuestionList[i].BelongsToSurveyID);
                    _srv.DeleteOptionsFromQuestion(AllQuestionList[i].QuestionUUID);
                }

                for (int k = 0; k < qnsOptions.Count; k++)
                {
                    _srv.AddOptionToQuestion(qnsOptions[k].OptionUUID,
                        qnsOptions[k].Text,
                        qnsOptions[k].BelongsToQuestionID);
                }

                return Redirect("View");
            }

            return Page();
        }
    }
}
