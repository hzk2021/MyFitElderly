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

        public List<Models.Survey.Question> qnsList { get; set; }

        public EditModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }

        public void OnGet(string sid)
        {
            svy = _srv.GetASurvey(sid);
            qnsList = _srv.GetQuestionsFromASurvey(sid);

            if (svy == null)
            {
                Response.Redirect("/Staff/Survey/View");
            }
        }
    }
}
