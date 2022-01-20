using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Survey
{
    public class CreateSurveyModel : PageModel
    {
        private SurveyService _srv;

        [BindProperty]
        public Models.Survey.Survey newSurvey { get; set; }

        [BindProperty]
        public List<Models.Survey.Question> AllQuestionList { get; set; }

        [BindProperty]
        public List<Models.Survey.QuestionOption> qnsOptions { get; set; }

        [BindProperty]
        public IFormFile imgFile { get; set; }

        public UserService _usrv;

        public CreateSurveyModel(SurveyService surveySrv, UserService userSrv)
        {
            _srv = surveySrv;
            _usrv = userSrv;
        }

        public string surveyUUID = Guid.NewGuid().ToString();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Models.Survey.Survey svy = new Models.Survey.Survey()
                {
                    SurveyUUID = newSurvey.SurveyUUID,
                    Category = newSurvey.Category,
                    Title = newSurvey.Title,
                    Description = newSurvey.Description,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    ViewStatus = newSurvey.ViewStatus,
                    CreatedByStaffID = _usrv.retrieveuserid(HttpContext.Session.GetString("user")) // Temp | change later
                };

                if (imgFile != null)
                {
                    using (var t = new MemoryStream())
                    {
                        imgFile.CopyTo(t);
                        svy.ImgBytes = t.ToArray();
                    }
                }

                await _srv.AddSurvey(svy);

                for (int i = 0; i < AllQuestionList.Count; i++)
                {
                    await _srv.AddQuestionToSurvey(AllQuestionList[i].QuestionUUID, AllQuestionList[i].Text, AllQuestionList[i].BelongsToSurveyID);
                    await _srv.DeleteOptionsFromQuestion(AllQuestionList[i].QuestionUUID);
                }

                for (int k = 0; k < qnsOptions.Count; k++)
                {
                    await _srv.AddOptionToQuestion(qnsOptions[k].OptionUUID,
                        qnsOptions[k].Text,
                        qnsOptions[k].BelongsToQuestionID);
                }

                return Redirect("View");
            }

            return Page();
        }
    }
}
