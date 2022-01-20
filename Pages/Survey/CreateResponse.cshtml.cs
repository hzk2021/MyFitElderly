using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Survey
{
    public class CreateResponseModel : PageModel
    {
        public SurveyService _srv;
        public List<Models.Survey.Question> AllQuestionList { get; set; }

        public Models.Survey.Survey survey { get; set; }

        [BindProperty]
        public List<Models.Survey.SurveyResponse> survey_response { get; set; }

        public CreateResponseModel(SurveyService surveySrv)
        {
            _srv = surveySrv;
        }

        public async void OnGet(string sid)
        {
            survey = await _srv.GetASurvey(sid);

            if (survey == null)
            {
                Response.Redirect("/Survey/List");
            }
            else
            {
                AllQuestionList = await _srv.GetQuestionsFromASurvey(survey.SurveyUUID);
            }
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < survey_response.Count; i++)
                {
                    var svy_response = survey_response[i];

                    svy_response.SurveyResponseUUID = Guid.NewGuid().ToString();
                    svy_response.Question_Text = svy_response.Question_Text;
                    svy_response.Response_Text = svy_response.Response_Text;
                    svy_response.ReferenceToSurveyID = svy_response.ReferenceToSurveyID;
                    svy_response.SubmittedByCustomerID = 7; // TEMP ID (MUST CHANGE LATER)

                    await _srv.AddSurveyResponse(svy_response);
                }

                return RedirectToPage("List");

            }
            return Page();
        }

    }
}
