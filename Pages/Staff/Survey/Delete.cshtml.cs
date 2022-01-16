using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Survey
{
    public class DeleteModel : PageModel
    {
        public SurveyService _srv;

        public DeleteModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }

        public void OnGet()
        {
            Response.Redirect("/Staff/Survey/View");
        }

        public async Task<IActionResult> OnPost(string sid)
        {
            if (sid != null && sid != string.Empty)
            {
                await _srv.DeleteSurveyQnsAndOptions(sid);
            }

            return Redirect("/Staff/Survey/View");
        }

    }
}
