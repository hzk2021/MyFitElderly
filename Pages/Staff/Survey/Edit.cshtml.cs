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
        private SurveyService _srv;

        [BindProperty]
        public Models.Survey.Survey svy { get; set; }

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

            //_srv.AddQuestionToSurvey("Hows test", "e2c6a30b-fea4-4348-9f31-d47e3a2aa4fe");
        }
    }
}
