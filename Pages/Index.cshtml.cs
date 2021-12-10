using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Pages
{
    public class IndexModel : PageModel
    {

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EDP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]

        public string cUsername { get; set; }
        


        public bool isStaff(string userID)
        {

            var role = "";

            string sql = "select * FROM [dbo].[User] WHERE [Username]=@USERID";
            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
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
                HttpContext.Session.Clear();




            }
            else
            {



                return RedirectToPage("Login");




            }

            return Page();

        }
    }
}
