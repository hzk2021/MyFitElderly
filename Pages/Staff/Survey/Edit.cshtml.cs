using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Survey
{
    public class EditModel : PageModel
    {
        public SurveyService _srv;

        [BindProperty]
        public Models.Survey.Survey svy { get; set; }

        [BindProperty]
        public List<Models.Survey.Question> AllQuestionList { get; set; }

        [BindProperty]
        public List<Models.Survey.QuestionOption> qnsOptions { get; set; }

        public EditModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }

        public void OnGet(string sid)
        {
            svy = _srv.GetASurvey(sid);

            if (svy == null)
            {
                Response.Redirect("/Staff/Survey/View");
            }
            else
            {
                AllQuestionList = _srv.GetQuestionsFromASurvey(svy.SurveyUUID);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _srv.UpdateSurvey(svy); // Title, Description, Category...

                _srv.DeleteQuestionsWithSurveyUUID(svy.SurveyUUID);

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

            }
            return Page();
        }
    }
}
