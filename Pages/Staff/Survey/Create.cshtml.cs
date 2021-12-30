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

        [BindProperty]
        public Models.Survey.Survey newSurvey { get; set; }

        public CreateSurveyModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Models.Survey.Survey svy = new Models.Survey.Survey()
                {
                    SurveyUUID = Guid.NewGuid().ToString(),
                    Category = newSurvey.Category,
                    Title = newSurvey.Title,
                    Description = newSurvey.Description,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    ViewStatus = false,
                    CreatedByStaffID = 1 // Temp | change later
                };
                _srv.AddSurvey(svy);

                return Redirect("View");
            }

            return Page();
        }
    }
}
