using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NLog;

namespace EDP_Project.Pages.Auth
{
    public class EndPasswordResetModel : PageModel
    {
        private static Logger logger = LogManager.GetLogger("MyAppLoggerRules");

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        [BindProperty]

        public string error_msg { get; set; }

        [BindProperty]

        public string userToken { get; set; } 

        [BindProperty]

        public string inputPass { get; set; }

        [BindProperty]

        public string oldPasswordHash { get; set; }


        [BindProperty]

        public string oldPasswordSalt { get; set; }

        [BindProperty]

        public string salt { get; set; }

        [BindProperty]

        public string finalHash { get; set; }




        protected string getUserEmail(string userTokenx)
        {
            string h = null;
            string sql = "select Email FROM User WHERE ResetPwToken = @RESETPWTOKEN";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@RESETPWTOKEN", userTokenx.Replace(" ", "+"));
            try
            {
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
            return h;
        }


        protected void verifyPwTokenOrSession()
        {

            userToken = HttpContext.Request.Query["token"].ToString();

            //HttpUtility.UrlDecode(HttpContext.Request.Query["token"]).Replace(" ", "+")


            if (userToken != null)
            {

                userToken = HttpUtility.UrlDecode(userToken).Replace(" ", "+");
                string dbPwToken = null;
                DateTime dbPwTokenExpiry = new DateTime();

                // 

                string sql = "SELECT * FROM User WHERE ResetPwToken=@RESETPWTOKEN";
                MySqlCommand command = new MySqlCommand(sql, con);
                command.Parameters.AddWithValue("@RESETPWTOKEN", userToken);
                try
                {
                    con.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            if (reader["ResetPwToken"] != DBNull.Value)
                            {
                                dbPwToken = reader["ResetPwToken"].ToString();
                            }

                            if (reader["ResetPwTokenExpiry"] != DBNull.Value)
                            {
                                dbPwTokenExpiry = (DateTime)reader["ResetPwTokenExpiry"];
                            }

                            if (reader["Password"] != DBNull.Value)
                            {
                                oldPasswordHash = reader["Password"].ToString();
                            }
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                oldPasswordSalt = reader["PasswordSalt"].ToString();
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


                // This checks whether the time validity of token has been expired, as well as whether it matches.
                // If doesn't match, redirect back to login page.

                // Todo check?

                if (DateTime.Compare(DateTime.Now, dbPwTokenExpiry) > 0 || !(userToken.Equals(dbPwToken)))

                    Response.Redirect("/Auth/VerificationFailed");
            }

        }

        public void OnGet()
        {



            verifyPwTokenOrSession();

        }

        public bool verifyPassAge()
        {

            DateTime lastPwSet = new DateTime();

            con.Open();


            try
            {
                MySqlCommand cmdx = new MySqlCommand("SELECT * FROM USER WHERE ResetPwToken=@RESETPWTOKEN", con);

                var meow = HttpUtility.UrlDecode(HttpContext.Request.Query["token"]);
                cmdx.Parameters.AddWithValue("@RESETPWTOKEN", userToken.Replace(" ", "+"));

                using (MySqlDataReader reader = cmdx.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["LastPwSet"] != null)
                        {
                            if (reader["LastPwSet"] != DBNull.Value)
                            {
                                lastPwSet = (DateTime)reader["LastPwSet"];
                            }

                            else
                            {
                                return true;
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
            // Minimum password age; if it has exceeded 3 days, then allow reset of password

            if (DateTime.Now.Subtract(lastPwSet).TotalDays > 3)
            {
                return true;
            }
            return false;

        }




        public bool verifySamePassword(string finalHashxx)
        {
            con.Open();

            string currentHashedPass = "";

            string oldPasswordSalt = "";



            // Need to get user's password hash & salt with their token only.

            try
            {
                MySqlCommand cmdx = new MySqlCommand("SELECT * FROM USER WHERE ResetPwToken=@RESETPWTOKEN", con);


                var meow = HttpUtility.UrlDecode(HttpContext.Request.Query["token"]);

                cmdx.Parameters.AddWithValue("@RESETPWTOKEN", userToken.Replace(" ", "+"));

                using (MySqlDataReader reader = cmdx.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Password"] != null)
                        {
                            if (reader["Password"] != DBNull.Value)
                            {
                                currentHashedPass = (String)reader["Password"];
                            }


                            if (reader["PasswordSalt"] != DBNull.Value)
                            {
                                oldPasswordSalt = (String)reader["PasswordSalt"];
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


            SHA512Managed hashing = new SHA512Managed();


            string currentPwdWithSalt = inputPass + oldPasswordSalt;

            byte[] currentHashWithSalt = hashing.ComputeHash(System.Text.Encoding.UTF8.GetBytes(currentPwdWithSalt));

            string userHash1 = Convert.ToBase64String(currentHashWithSalt);


            if (userHash1.Equals(currentHashedPass))
            {
                return true;
            }


            return false;




        }


        private int CheckPassword(string password)
        {
            int score = 0;

            //todo: implementation here


            //score 1

            if (password.Length < 8)
            {
                return 1;
            }

            else
            {
                score = 1;
            }

            //score 2

            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }

            //score 3


            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }

            //score 4


            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }


            //score 5


            if (Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                score++;
            }



            return score;

        }



        public IActionResult OnPost()
        {

            userToken = HttpContext.Request.Query["token"].ToString();




            int scores = CheckPassword(inputPass);


            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Excellent";
                    break;
                default:
                    break;
            }

            if (scores < 4)
            {
                error_msg = "Please enter a stronger password.";
            }


            if (status == "Excellent")
            {


                if (verifyPassAge())
                {

                    if (!verifySamePassword(inputPass))
                    {


                        // Change Password.

                        con.Open();
                        try
                        {

                            // Make a random salt
                            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                            byte[] saltByte = new byte[8];

                            //Fills array of bytes with a cryptographically strong sequence of random values.
                            rng.GetBytes(saltByte);
                            salt = Convert.ToBase64String(saltByte);

                            SHA512Managed hashing = new SHA512Managed();
                            string passWithSalt = inputPass + salt;
                            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passWithSalt));
                            finalHash = Convert.ToBase64String(hashWithSalt);





                            try
                            {

                                string sql2 = "UPDATE User SET Password = @PASSWORDHASH, PasswordSalt = @PASSWORDSALT ,ResetPwTokenExpiry = @RESETPWTOKENEXPIRY WHERE ResetPwToken=@RESETPWTOKEN; ";
                                using (var cmdx = new MySqlCommand(sql2, con))
                                {
                                    var meow = HttpUtility.UrlDecode(HttpContext.Request.Query["token"]);
                                    var meow2 = salt;
                                    var meow3 = finalHash;
                                    //var meow4 = userToken.Replace(" ", "+");

                                    cmdx.Parameters.AddWithValue("@RESETPWTOKEN", userToken.Replace(" ", "+"));
                                    cmdx.Parameters.AddWithValue("@PASSWORDSALT", salt);
                                    cmdx.Parameters.AddWithValue("@PASSWORDHASH", finalHash);
                                    cmdx.Parameters.AddWithValue("@RESETPWTOKENEXPIRY", DBNull.Value);

                                    var update = cmdx.ExecuteNonQuery();
                                }
                            }

                            catch (Exception ex)
                            {
                                throw new Exception(ex.ToString());
                            }

                            finally
                            {

                                logger.Warn($"{getUserEmail(userToken.Replace(" ", "+"))} Changed their password manually");


                                // Void the last token & update pw last set timestamp

                                string sql = "UPDATE User SET FailedAttempts = 0, LastPwSet = @LASTPWSET, ResetPwToken = @PWTOKEN WHERE ResetPwToken=@RESETPWTOKEN; ";
                                using (var cmdz = new MySqlCommand(sql, con))
                                {
                                    cmdz.Parameters.AddWithValue("@RESETPWTOKEN", HttpUtility.UrlDecode(userToken).Replace(" ", "+"));
                                    cmdz.Parameters.AddWithValue("@LASTPWSET", DateTime.Now);
                                    cmdz.Parameters.AddWithValue("@PWTOKEN", DBNull.Value);
                                    cmdz.Parameters.AddWithValue("@LASTFAILED", DBNull.Value);

                                    var update = cmdz.ExecuteNonQuery();
                                }

                                con.Close();


                                // here
                                // cannot retrieve name from session, cuz they might be logged out



                                //Response.Redirect("/Login?passUpdated=true", false);



                                Response.Redirect("/Logout", false);



                            }



                        }

                        catch (Exception ex)
                        {
                            throw new Exception(ex.ToString());
                        }


                    }

                    else
                    {
                        error_msg = "Your cannot be the same as your recent password.";

                    }

                }


                else
                {
                    // Change error message todo

                    error_msg = "You must wait longer before changing your password.";
                }
            }

            else
            {
                // Change error message todo

                error_msg = "Please enter a stronger password.";
            }

                return Page();


        }

    }
}
