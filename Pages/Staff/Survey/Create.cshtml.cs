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
        public string test { get; set; }

        public CreateSurveyModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }
        public void OnGet()
        {
            test = _srv.GetAllSurveys().Count.ToString();
        }
    }
}
