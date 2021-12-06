using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace EDP_Project.Pages.Auth
{

    //Todo store user object in session

    // Hash password, store in DB

    // Email Verification


    public class LoginModel : PageModel
    {
        [BindProperty]

        public string userID { get; set; }

        [BindProperty]
        public string userPass { get; set; }

        [BindProperty]
        public string error_msg { get; set; }



        //[BindProperty]


        //public User currentUser { get; set; }


        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EDP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        //Get DB salt


        protected string getDBSalt(string userid)
        {
            string s = null;
            string sql = "select PasswordSalt FROM [dbo].[User] WHERE [Email]=@USERID";
            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                s = reader["PasswordSalt"].ToString();
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
            return s;
        }

        // Get DB Hash
        protected string getDBHash(string userid)
        {
            string h = null;
            string sql = "select PasswordHash FROM [dbo].[User] WHERE [Email]=@USERID";
            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
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


        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {




            string dbHash = "";
            string dbSalt = "";

            // Open DB,
            con.Open();

            SqlCommand check_User_Name = new SqlCommand("SELECT * FROM [dbo].[User] WHERE ([Email] = @userId)", con);
            check_User_Name.Parameters.AddWithValue("@userId", userID);
            int UserExist = (int)check_User_Name.ExecuteScalar();
            string dbPass = "";
            string currentUser = "";

            try
            {
                using (SqlDataReader reader = check_User_Name.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Username"] != null)
                        {
                            if (reader["Username"] != DBNull.Value)
                            {
                                currentUser = reader["Username"].ToString();
                            }

                        }

                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                dbSalt = reader["PasswordSalt"].ToString();
                            }

                        }

                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                dbHash = reader["PasswordHash"].ToString();
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            if (UserExist > 0)
            {

                // if password wrong, increase the thing.

                // If corrett, goo bitch

                SHA512Managed hashing = new SHA512Managed();
                string pwdWithSalt = userPass + dbSalt;
                byte[] hashWithSalt = hashing.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwdWithSalt));
                string userHash = Convert.ToBase64String(hashWithSalt);

                if (userHash.Equals(dbHash))
                {

                    HttpContext.Session.SetString("user", currentUser);
                    return RedirectToPage("Index");

                }

                else
                {

                    error_msg = "Invalid email or password!";
                    return Page();
                }


                //HttpContext.Session.SetString("user", "username");



            }
            else
            {


                error_msg = "Invalid email or password!";

                //Username doesn't exist.
                return Page();
            }




            // Check whether user exists,


            // if user exists generate session


            // Otherwise 


            // Return error
            return Page();
        }

    }
}
