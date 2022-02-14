using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Survey
{
    public class DeleteResponsesModel : PageModel
    {
        public SurveyService _srv;

        public UserService _usrv;

        public DeleteResponsesModel(SurveyService surveySrv, UserService userSrv)
        {
            _srv = surveySrv;
            _usrv = userSrv;
        }

        public void OnGet()
        {
            Response.Redirect("/Staff/Survey/View");
        }
        public async Task<IActionResult> OnPost(string sid)
        {
            _usrv.AIOCheckStaff();

            if (sid != null && sid != string.Empty)
            {
                await _srv.DeleteResponseForASurvey(sid);
                return Redirect($"/Staff/Survey/ViewResponses/{sid}");
            }
            else
            {
                return Redirect("/Staff/Survey/View");
            }

        }
    }
}
