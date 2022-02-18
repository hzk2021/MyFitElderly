using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Pages
{
    public class IndexModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        private readonly UserService _userSvc;


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserService userSvc)
        {
            _logger = logger;
            _userSvc = userSvc;

        }

        [BindProperty]

        public string cUsername { get; set; }

        [BindProperty]

        public string cVerified { get; set; }





        public int retrieveuserid(string userID)
        {
            int uruserid = 0;
            string sql = "select * FROM User WHERE Username=@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Id"] != null)
                        {
                            if (reader["Id"] != DBNull.Value)
                            {
                                uruserid = (int)reader["Id"];
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
                con.Close();
            }
            return uruserid;

        }


        public bool isStaff(string userID)
        {
            var role = "";

            string sql = "select * FROM User WHERE Username=@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Role"] != null)
                        {
                            if (reader["Role"] != DBNull.Value)
                            {
                                role = reader["Role"].ToString();
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
                con.Close(); 
            }


            if (role == "Staff")
            {
                return true;
            }


            return false;
        }





        public IActionResult OnGet()
        {



            if (HttpContext.Session.GetString("user") != null) // if user not logged in
            {

                if (!_userSvc.twofactorVerified(HttpContext.Session.GetString("user"))) // if user didnt verify via 2FA
                {

                    return RedirectToPage("/Auth/TwoFactorAuth");

                }

                else
                {
                    if (!_userSvc.emailVerified(HttpContext.Session.GetString("user"))) // if user did not verify email
                    {
                        return RedirectToPage("/Auth/EmailVerification");
                    }


                    if (_userSvc.exceededPwAge(HttpContext.Session.GetString("user"))) /// if user pw age exceeded
                    {
                        return RedirectToPage("/Auth/ResetPassword");
                    }
                }
            }
            else
            {
                return Redirect("/Login");
            }



            return Page();

        }
    }
}
