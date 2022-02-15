using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff
{
    public class WADashboardModel : PageModel
    {

        public SurveyService _srv;

        public UserService _usrv;

        public BlogService _blsrv;

        public BookingService _bksrv;

        public HealthService _hsrv;

        public int totalUsers { get; set; }

        public int maleCount { get; set; }

        public int femaleCount { get; set; }

        private List<Models.Survey.Survey> surveysCreated { get; set; }

        public int surveyCount { get; set; }

        public int blogCount { get; set; }

        public int foodCount { get; set; }

        public WADashboardModel(SurveyService surveySrv, UserService userSrv, BlogService blogSrv, BookingService bookingService, HealthService healthService)
        {
            _srv = surveySrv;
            _usrv = userSrv;
            _blsrv = blogSrv;
            _bksrv = bookingService;
            _hsrv = healthService;
        }

        public async void OnGet()
        {
            _usrv.AIOCheckStaff();

            surveysCreated = await _srv.GetAllSurveys();
            surveyCount = surveysCreated.Count;

            blogCount = _blsrv.GetAllBlogPost().Count;
            foodCount = _hsrv.GetAllFoodRecords().Count;

            totalUsers = GetUsersCount();
            maleCount = GetMalesFromUserDB();
            femaleCount = GetFemalesFromUserDB();         
        }


        MySqlConnection connection = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        private int GetMalesFromUserDB()
        {
            int maleCount = 0;
            string sql = "SELECT * FROM USER;";
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Gender"] != null)
                        {
                            if (reader["Gender"] != DBNull.Value)
                            {
                                if (reader["Gender"].ToString() == "Male")
                                {
                                    maleCount++;
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return maleCount;
        }

        private int GetFemalesFromUserDB()
        {
            int femaleCount = 0;
            string sql = "SELECT * FROM USER;";
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Gender"] != null)
                        {
                            if (reader["Gender"] != DBNull.Value)
                            {
                                if (reader["Gender"].ToString() == "Female")
                                {
                                    femaleCount++;
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return femaleCount;
        }

        private int GetUsersCount()
        {
            int userCount = 0;
            string sql = "SELECT * FROM USER;";
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Gender"] != null)
                        {
                            if (reader["Gender"] != DBNull.Value)
                            {
                                userCount++;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return userCount;
        }


    }
}
