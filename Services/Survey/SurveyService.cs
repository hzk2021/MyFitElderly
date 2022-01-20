using Microsoft.EntityFrameworkCore;
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

        public async Task AddSurvey(Models.Survey.Survey survey)
        {
            await _dbcontext.Survey.AddAsync(survey);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<Models.Survey.Survey>> GetAllSurveys()
        {
            return await _dbcontext.Survey.ToListAsync(); // Test
        }

        public async Task<Models.Survey.Survey> GetASurvey(string surveyUUID)
        {
            return await _dbcontext.Survey.FirstOrDefaultAsync(c => c.SurveyUUID == surveyUUID);
        }

        public async Task UpdateSurvey(Models.Survey.Survey survey)
        {
            var originalSurvey = _dbcontext.Survey.FirstOrDefault(s => s.SurveyUUID == survey.SurveyUUID);
            if (originalSurvey != null)
            {
                originalSurvey.Category = survey.Category;
                originalSurvey.Title = survey.Title;
                originalSurvey.Description = survey.Description;
                originalSurvey.ViewStatus = survey.ViewStatus;
                originalSurvey.UpdatedOn = DateTime.Now;

                await _dbcontext.SaveChangesAsync();

            }
        }

        public async Task DeleteQuestionsWithSurveyUUID(string surveyUUID)
        {
            var listOfQuestions = _dbcontext.Question.Where(q => q.BelongsToSurveyID == surveyUUID).ToList();
            foreach (var qns in listOfQuestions)
            {
                await DeleteOptionsFromQuestion(qns.QuestionUUID);

                _dbcontext.Question.Remove(qns);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task DeleteOptionsFromQuestion(string questionUUID)
        {
            var listOfOptions = _dbcontext.QuestionOption.Where(qo => qo.BelongsToQuestionID == questionUUID).ToList();
            foreach (var option in listOfOptions)
            {
                _dbcontext.QuestionOption.Remove(option);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task AddQuestionToSurvey(string questionUUID, string questionText, string surveyUUID)
        {
            Models.Survey.Question qns = new Models.Survey.Question()
            {
                QuestionUUID = questionUUID,
                Text = questionText,
                BelongsToSurveyID = surveyUUID
            };

            await _dbcontext.Question.AddAsync(qns);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<Models.Survey.Question>> GetQuestionsFromASurvey(string surveyUUID)
        {
            return await _dbcontext.Question.Where(q => q.BelongsToSurveyID == surveyUUID).ToListAsync();
        }

        public async Task UpdateQuestion(Models.Survey.Question question)
        {
            var originalQuestion = _dbcontext.Question.FirstOrDefault(q => q.QuestionUUID == question.QuestionUUID);
            if (originalQuestion != null)
            {
                originalQuestion.Text = question.Text;

                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task AddOptionToQuestion(string optionUUID, string questionOptionText, string questionUUID)
        {
            Models.Survey.QuestionOption qnsOption = new Models.Survey.QuestionOption()
            {
                OptionUUID = optionUUID,
                Text = questionOptionText,
                BelongsToQuestionID = questionUUID
            };

            await _dbcontext.QuestionOption.AddAsync(qnsOption);
            await _dbcontext.SaveChangesAsync();

        }

        public async Task<List<Models.Survey.QuestionOption>> GetAllQuestionOptions()
        {
            return await _dbcontext.QuestionOption.ToListAsync();
        }
        public async Task<List<Models.Survey.QuestionOption>> GetOptionsFromAQuestion(string questionUUID)
        {
            return await _dbcontext.QuestionOption.Where(q => q.BelongsToQuestionID == questionUUID).ToListAsync();
        }

        public async Task DeleteSurveyQnsAndOptions(string surveyUUID)
        {
            var survey = await GetASurvey(surveyUUID);

            if (survey != null)
            {
                var listOfQns = await GetQuestionsFromASurvey(surveyUUID);

                if (listOfQns != null)
                {
                    for (int i = 0; i < listOfQns.Count; i++)
                    {
                        var listOfOptions = await GetOptionsFromAQuestion(listOfQns[i].QuestionUUID);

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
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task AddSurveyResponse(Models.Survey.SurveyResponse srv_response)
        {
            await _dbcontext.SurveyResponse.AddAsync(srv_response);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
