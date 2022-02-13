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
    public class ViewModel : PageModel
    {
        private SurveyService _srv;
        public List<Models.Survey.Survey> Surveys { get; set; }
        public UserService _usrv;

        public ViewModel(SurveyService surveySrv, UserService userSrv)
        {
            _srv = surveySrv;
            _usrv = userSrv;
        }

        public async void OnGet()
        {
            _usrv.AIOCheck();
            Surveys = await _srv.GetAllSurveys();
        }
    }
}
