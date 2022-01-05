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

        public void UpdateSurvey(Models.Survey.Survey survey)
        {
            var originalSurvey = _dbcontext.Survey.FirstOrDefault(s => s.SurveyUUID == survey.SurveyUUID);
            if (originalSurvey != null)
            {
                originalSurvey.Category = survey.Category;
                originalSurvey.Title = survey.Title;
                originalSurvey.Description = survey.Description;
                originalSurvey.UpdatedOn = DateTime.Now;

                _dbcontext.SaveChanges();

            }
        }

        public void DeleteQuestionsWithSurveyUUID(string surveyUUID)
        {
            var listOfQuestions = _dbcontext.Question.Where(q => q.BelongsToSurveyID == surveyUUID).ToList();
            foreach (var qns in listOfQuestions)
            {
                DeleteOptionsFromQuestion(qns.QuestionUUID);

                _dbcontext.Question.Remove(qns);
                _dbcontext.SaveChanges();
            }
        }

        public void DeleteOptionsFromQuestion(string questionUUID)
        {
            var listOfOptions = _dbcontext.QuestionOption.Where(qo => qo.BelongsToQuestionID == questionUUID).ToList();
            foreach (var option in listOfOptions)
            {
                _dbcontext.QuestionOption.Remove(option);
                _dbcontext.SaveChanges();
            }
        }

        public void AddQuestionToSurvey(string questionUUID, string questionText, string surveyUUID)
        {
            Models.Survey.Question qns = new Models.Survey.Question()
            {
                QuestionUUID = questionUUID,
                Text = questionText,
                BelongsToSurveyID = surveyUUID
            };

            _dbcontext.Question.Add(qns);
            _dbcontext.SaveChanges();
        }

        public List<Models.Survey.Question> GetQuestionsFromASurvey(string surveyUUID)
        {
            return _dbcontext.Question.Where(q => q.BelongsToSurveyID == surveyUUID).ToList();
        }

        public void UpdateQuestion(Models.Survey.Question question)
        {
            var originalQuestion = _dbcontext.Question.FirstOrDefault(q => q.QuestionUUID == question.QuestionUUID);
            if (originalQuestion != null)
            {
                originalQuestion.Text = question.Text;

                _dbcontext.SaveChanges();
            }
        }

        public void AddOptionToQuestion(string optionUUID, string questionOptionText, string questionUUID)
        {
            Models.Survey.QuestionOption qnsOption = new Models.Survey.QuestionOption()
            {
                OptionUUID = optionUUID,
                Text = questionOptionText,
                BelongsToQuestionID = questionUUID
            };

            _dbcontext.QuestionOption.Add(qnsOption);
            _dbcontext.SaveChanges();

        }

        public List<Models.Survey.QuestionOption> GetAllQuestionOptions()
        {
            return _dbcontext.QuestionOption.ToList();
        }
        public List<Models.Survey.QuestionOption> GetOptionsFromAQuestion(string questionUUID)
        {
            return _dbcontext.QuestionOption.Where(q => q.BelongsToQuestionID == questionUUID).ToList();
        }

        public void DeleteSurveyQnsAndOptions(string surveyUUID)
        {
            var survey = GetASurvey(surveyUUID);

            if (survey != null)
            {
                var listOfQns = GetQuestionsFromASurvey(surveyUUID);

                if (listOfQns != null)
                {
                    for (int i = 0; i < listOfQns.Count; i++)
                    {
                        var listOfOptions = GetOptionsFromAQuestion(listOfQns[i].QuestionUUID);

                        if (listOfOptions != null)
                        {
                            foreach (var option in listOfOptions)
                            {
                                
                                _dbcontext.QuestionOption.Remove(option);
                                _dbcontext.SaveChanges();
                            }
                        }

                        _dbcontext.Question.Remove(listOfQns[i]);
                        _dbcontext.SaveChanges();
                    }
                }

                _dbcontext.Survey.Remove(survey);
                _dbcontext.SaveChanges();
            }
        }

    }
}
