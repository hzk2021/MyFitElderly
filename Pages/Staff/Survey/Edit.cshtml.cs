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
        public List<Models.Survey.Question> qnsList { get; set; }

        [BindProperty]
        public Dictionary<string, List<Models.Survey.QuestionOption>> qnsOptionList { get; set; }
        public EditModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
            qnsOptionList = new Dictionary<string, List<Models.Survey.QuestionOption>>();
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
                qnsList = _srv.GetQuestionsFromASurvey(svy.SurveyUUID);
            }
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                _srv.UpdateSurvey(svy);

                foreach (var qns in qnsList)
                {
                    _srv.UpdateQuestion(qns);
                }

                ////foreach (var item in qnsOptionList)
                ////{
                ////    _srv.AddOptionToQuestion(qnsOptionList.Count.ToString(), item.Key);
                ////}
            }
            return Page();
        }
    }
}
