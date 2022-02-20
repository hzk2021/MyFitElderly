using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Survey
{
    public class ListModel : PageModel
    {
        private SurveyService _srv;
        public List<Models.Survey.Survey> Surveys { get; set; }

        private UserService _usrv;

        public ListModel(SurveyService surveySrv, UserService userService)
        {
            _srv = surveySrv;
            _usrv = userService;
        }

        public async void OnGet()
        {
            //_usrv.AIOCheckGuest();
            Surveys = await _srv.GetAllSurveys();
        }
    }
}
