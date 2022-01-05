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


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]

        public string cUsername { get; set; }

        [BindProperty]

        public string cVerified { get; set; }



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

            if (HttpContext.Session.GetString("user") != null)
            {


                if (isStaff(HttpContext.Session.GetString("user")))
                {
                    // whatevs u want
                }



                cUsername = HttpContext.Session.GetString("user");
                //HttpContext.Session.Clear();




            }
            else
            {



                return RedirectToPage("Login");




            }

            return Page();

        }
    }
}
