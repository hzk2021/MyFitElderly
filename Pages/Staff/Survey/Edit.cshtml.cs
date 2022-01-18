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

        public async void OnGet(string sid)
        {
            svy = await _srv.GetASurvey(sid);

            if (svy == null)
            {
                Response.Redirect("/Staff/Survey/View");
            }
            else
            {
                AllQuestionList = await _srv.GetQuestionsFromASurvey(svy.SurveyUUID);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _srv.UpdateSurvey(svy); // Title, Description, Category...

                await _srv.DeleteQuestionsWithSurveyUUID(svy.SurveyUUID);

                for (int i = 0; i < AllQuestionList.Count; i++)
                {
                    await _srv.AddQuestionToSurvey(AllQuestionList[i].QuestionUUID, AllQuestionList[i].Text, AllQuestionList[i].BelongsToSurveyID);
                    await _srv.DeleteOptionsFromQuestion(AllQuestionList[i].QuestionUUID);
                }

                for (int k = 0; k < qnsOptions.Count; k++)
                {
                    await _srv.AddOptionToQuestion(qnsOptions[k].OptionUUID,
                        qnsOptions[k].Text,
                        qnsOptions[k].BelongsToQuestionID);
                }

                return RedirectToPage("View");

            }
            return Page();
        }
    }
}
