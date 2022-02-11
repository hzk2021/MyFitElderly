using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NLog;

namespace EDP_Project.Pages
{
    public class LogoutModel : PageModel
    {

        private static Logger logger = LogManager.GetLogger("MyAppLoggerRules");

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");



        protected string getUserEmail(string userid)
        {
            string h = null;
            string sql = "select Email FROM User WHERE Username = @USERNAME";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERNAME", userid);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["Email"] != null)
                        {
                            if (reader["Email"] != DBNull.Value)
                            {
                                h = reader["Email"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { con.Close(); }
            return h;
        }


        public void unverifyUser(string userid)
        {

            con.Open();


            try
            {
                string sql = "UPDATE User SET TwoFactorVerified = @VERIFIED WHERE Username=@USERID; ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@USERID", userid);
                    cmd.Parameters.AddWithValue("@VERIFIED", 0);
                    var update = cmd.ExecuteNonQuery();
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


        }

        public IActionResult OnGet()
        {


            unverifyUser(HttpContext.Session.GetString("user").ToString());

            logger.Info($"{getUserEmail(HttpContext.Session.GetString("user").ToString())} logged out");
            HttpContext.Session.Remove("user");
            HttpContext.Session.Clear();
            return RedirectToPage("Login", false);

        }



    }
}
