using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Web;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Auth
{




    public class LoginModel : PageModel
    {
        [BindProperty]

        public string userID { get; set; }

        [BindProperty]
        public string userPass { get; set; }

        [BindProperty]
        public string error_msg { get; set; }



        public class MyObject
        {
            public string success { get; set; }

            public List<String> ErrorMessage { get; set; }
        }





        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        //SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EDP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



        // Captcha Validation - to prevent attacks

        public bool ValidateCaptcha()
        {
            bool result = true;
            string captchaResponse = Request.Form["g-recaptcha-response"];


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                (" https://www.google.com/recaptcha/api/siteverify?secret=6LfLmX8dAAAAAOr5H4_NpVIsM3z4tszdaL_UbMjn &response=" + captchaResponse
                );

            try
            {
                //Code to receive the Response in JSON format from google server

                using (WebResponse wResponse = req.GetResponse())
                {

                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        //The response in JSON format
                        string jsonResponse = readStream.ReadToEnd();

                        // To show the JSON response string for learning purpsoe

                        //lblMessage.Text = jsonResponse.ToString();


                        // Create jsonObject to handle the response either success or error
                        // Deserialize json

                        MyObject jsonObject = JsonConvert.DeserializeObject<MyObject>(jsonResponse);

                        //Convert the string "False" to bool false or "true" to bool true

                        result = Convert.ToBoolean(jsonObject.success);
                    }

                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }


        //Get DB salt


        protected string getDBSalt(string userid)
        {
            string s = null;
            string sql = "select PasswordSalt FROM User WHERE Email@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
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
            string sql = "select PasswordHash FROM User WHERE Email@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
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

            if (ValidateCaptcha())
            {



                string dbHash = "";
                string dbSalt = "";

                // Open DB,
                con.Open();



                //cmd.CommandText = "SELECT id from residentes WHERE nome = @nome";
                //cmd.Parameters.AddWithValue("@nome", nomeres);

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM USER WHERE EMAIL = @userId", con);
                cmd.Parameters.AddWithValue("@userId", userID);
                int UserExist = Convert.ToInt32(cmd.ExecuteScalar());

                if (UserExist > 0)
                {


                    MySqlCommand cmdx = new MySqlCommand("SELECT * FROM USER WHERE EMAIL = @userId", con);
                    cmdx.Parameters.AddWithValue("@userId", userID);
                    cmdx.ExecuteScalar();
                    string dbPass = "";
                    string currentUser = "";

                    try
                    {
                        using (MySqlDataReader reader = cmdx.ExecuteReader())
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

                                if (reader["Password"] != null)
                                {
                                    if (reader["Password"] != DBNull.Value)
                                    {
                                        dbHash = reader["Password"].ToString();
                                    }

                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }


 
                        // if password wrong, increase the thing.


                        SHA512Managed hashing = new SHA512Managed();
                        string pwdWithSalt = userPass + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);

                        if (userHash.Equals(dbHash))
                        {




                            HttpContext.Session.SetString("user", currentUser.Trim());
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

            else
            {
                error_msg = "Invalid captcha attempt!";
                return Page();


            }


        }

    }
}
