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

        public IActionResult OnGet()
        {


            


            logger.Info($"{getUserEmail(HttpContext.Session.GetString("user").ToString())} logged out");
            HttpContext.Session.Remove("user");
            HttpContext.Session.Clear();
            return RedirectToPage("Login", false);

        }



    }
}
