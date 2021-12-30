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

    }
}
