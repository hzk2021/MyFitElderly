using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Auth
{
    public class EndPasswordResetModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


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


        public IActionResult OnPost()
        {

            userToken = HttpContext.Request.Query["token"].ToString();


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
                    // Void the last token & update pw last set timestamp

                    string sql = "UPDATE User SET ResetPwToken = @PWTOKEN WHERE ResetPwToken=@RESETPWTOKEN; ";
                    using (var cmdz = new MySqlCommand(sql, con))
                    {
                        cmdz.Parameters.AddWithValue("@RESETPWTOKEN", HttpUtility.UrlDecode(userToken).Replace(" ", "+"));
                        cmdz.Parameters.AddWithValue("@PWTOKEN", DBNull.Value);
                        var update = cmdz.ExecuteNonQuery();
                    }

                    con.Close();

                    Response.Redirect("/Login?passUpdated=true", false);


                }



            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            return Page();


        }

    }
}
