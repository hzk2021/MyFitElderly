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
        public List<Dictionary<string, List<Models.Survey.QuestionOption>>> qnsANDoptions { get; set; }

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

                foreach (var qns in AllQuestionList)
                {
                    _srv.AddQuestionToSurvey(qns.QuestionUUID, qns.Text, qns.BelongsToSurveyID);
                }

                foreach (var t in qnsANDoptions)
                {
                    foreach (var pair in t)
                    {
                        _srv.DeleteOptionsFromQuestion(pair.Key);

                        foreach (var option in pair.Value)
                        {
                            _srv.AddOptionToQuestion(option.OptionUUID, option.Text, option.BelongsToQuestionID);
                        }
                    }
                }
            }
            return Page();
        }
    }
}
