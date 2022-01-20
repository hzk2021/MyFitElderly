using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Survey
{
    public class ListModel : PageModel
    {
        private SurveyService _srv;
        public List<Models.Survey.Survey> Surveys { get; set; }

        public ListModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }

        public async void OnGet()
        {
            Surveys = await _srv.GetAllSurveys();
        }
    }
}
