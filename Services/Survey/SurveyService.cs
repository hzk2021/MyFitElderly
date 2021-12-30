using Pract2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Services.Survey
{
    public class SurveyService
    {
        // UserDbContext is actually just DbContext
        private readonly UserDbContext _dbcontext;

        // DI
        public SurveyService(UserDbContext context)
        {
            _dbcontext = context;
        }

        public void AddSurvey(Models.Survey.Survey survey)
        {
            _dbcontext.Survey.Add(survey);
            _dbcontext.SaveChanges();
        }

        public List<Models.Survey.Survey> GetAllSurveys()
        {
            return _dbcontext.Survey.ToList(); // Test
        }

        public Models.Survey.Survey GetASurvey(string surveyUUID)
        {
            return _dbcontext.Survey.FirstOrDefault(c => c.SurveyUUID == surveyUUID);
        }
        public void AddQuestionToSurvey(string questionText, string surveyUUID)
        {
            Models.Survey.Question qns = new Models.Survey.Question()
            {
                QuestionUUID = Guid.NewGuid().ToString(),
                Text = questionText,
                BelongsToSurveyID = surveyUUID
            };

            _dbcontext.Question.Add(qns);
            _dbcontext.SaveChanges();
        }

    }
}
