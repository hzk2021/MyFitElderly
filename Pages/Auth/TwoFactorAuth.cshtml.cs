using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NLog;

namespace EDP_Project.Pages.Auth
{
    public class TwoFactorAuthModel : PageModel
    {
        private static Logger logger = LogManager.GetLogger("MyAppLoggerRules");


        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");
        string userID = null;


        [BindProperty(SupportsGet = true)]

        // secret ingredient is hash of passwordSalt + userID
        public string secretIngredient { get; set; }


        [BindProperty]

        public string error_msg { get; set; }

        [BindProperty]

        public string value1 { get; set; }

        [BindProperty]

        public string value2 { get; set; }

        [BindProperty]

        public string value3 { get; set; }

        [BindProperty]

        public string value4 { get; set; }

        [BindProperty]

        public string value5 { get; set; }

        [BindProperty]

        public string value6 { get; set; }

        [BindProperty]

        public string cUsername { get; set; }




        public string retrieveSecretStuff(string userid)
        {
            // Retrieve password salt, append it to userID & hash it.
            // Then return

            // yeah science, mr white!

            string h = null;
            string sql = "select PasswordSalt FROM User WHERE Username =@USERID";
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
                                h = reader["PasswordSalt"].ToString();
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




            SHA512Managed hashing = new SHA512Managed();
            string idWithSalt = userid + h;
            byte[] hashIDWithSalt = hashing.ComputeHash(System.Text.Encoding.UTF8.GetBytes(idWithSalt));
            string secretIngredient = Convert.ToBase64String(hashIDWithSalt);
            return secretIngredient;

        }




        public void verifyTwoFactor(string userid)
        {

            con.Open();


            try
            {
                string sql = "UPDATE User SET TwoFactorVerified = @VERIFIED WHERE Username=@USERID; ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@USERID", userid);
                    cmd.Parameters.AddWithValue("@VERIFIED", 1);
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







        public void OnGet()
        {
            HttpContext.Session.SetString("affirmation", "true");

        }


        public IActionResult OnPost()
        {
            var fuck = secretIngredient;

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string userInput = value1 + value2 + value3 + value4 + value5 + value6;

            var nigger = tfa.GetCurrentPIN(secretIngredient);

            if (tfa.GetCurrentPIN(secretIngredient) == userInput)
            {

                verifyTwoFactor(HttpContext.Session.GetString("user"));


                HttpContext.Session.Remove("affirmation");

                return RedirectToPage("/Index");
            }
            else
            {
                error_msg = "Invalid OTP. Please try again! :)";
            }

            return Page();
        }




    }
}
