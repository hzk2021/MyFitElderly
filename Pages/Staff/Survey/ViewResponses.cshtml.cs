using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDP_Project.Services;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EDP_Project.Pages.Staff.Survey
{
    public class ViewResponsesModel : PageModel
    {
        public SurveyService _srv;

        [BindProperty]
        public Models.Survey.Survey svy { get; set; }

        public UserService _usrv;

        [BindProperty]
        public List<Models.Survey.SurveyResponse> svyResponses { get; set; }

        public Dictionary<string, List<string>> svyResponseGroup { get; set; }

        public List<string> responseQuestions { get; set; }

        public ViewResponsesModel(SurveyService surveySrv, UserService userSrv)
        {
            _srv = surveySrv;
            _usrv = userSrv;
        }
        public async void OnGet(string sid)
        {
            _usrv.AIOCheckStaff();

            svy = await _srv.GetASurvey(sid);

            if (svy == null)
            {
                Response.Redirect("/Staff/Survey/View");
            }
            else
            {
                svyResponses = await _srv.GetResponsesFromSurveyID(svy.SurveyUUID);
                svyResponseGroup = new Dictionary<string, List<string>>();

                foreach (var response in svyResponses)
                {
                    var qns = response.Question_Text;

                    var answers = new List<string>();

                    foreach (var response_nested in svyResponses)
                    {
                        if (response_nested.Question_Text == qns)
                        {
                            answers.Add(response_nested.Response_Text);
                        }
                    }

                    if (!svyResponseGroup.ContainsKey(qns))
                    {
                        svyResponseGroup.Add(qns, answers);
                    } else
                    {
                        svyResponseGroup[qns] = answers;
                    }
                }
            }

        }

        public async Task<ActionResult> OnGetDownload(string SurveyUUID)
        {
            var existingSurvey = await _srv.GetASurvey(SurveyUUID);
            var svy_responses = await _srv.GetResponsesFromSurveyID(existingSurvey.SurveyUUID);

            string txt = "Question" + ",Response\n";
            foreach (var response in svy_responses)
            {
                txt = txt + ($"{response.Question_Text},{response.Response_Text}\n");
            }
            return File(Encoding.UTF8.GetBytes(txt), "text/plain", $"{existingSurvey.Title}_{existingSurvey.Category}_DataSet.csv");
        }
    }
}
