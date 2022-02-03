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
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Auth
{




    public class ResetPasswordModel : PageModel
    {

        [BindProperty]

        public string userPwInput { get; set; }

        [BindProperty]

        public string error_msg { get; set; }

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public bool verifyPassAge(string userId)
        {

            DateTime lastPwSet = new DateTime();

            con.Open();


            try
            {
                MySqlCommand cmdx = new MySqlCommand("SELECT * FROM USER WHERE USERNAME = @USERNAME", con);
                cmdx.Parameters.AddWithValue("@USERNAME", userId);
                cmdx.ExecuteScalar();

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

            // Minimum password age; if it has exceeded 3 days, then allow reset of password

            if (DateTime.Now.Subtract(lastPwSet).TotalDays > 3)
            {
                return true;
            }
            return false;

        }

        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToPage("/Login", false);
            }

            if (!verifyPassAge(HttpContext.Session.GetString("user").ToString()))
            {
                return RedirectToPage("/Index", false);

            }


            return Page();
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
            int scores = CheckPassword(userPwInput);
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




                string salt;
                string finalHash;
                byte[] Key;
                byte[] IV;

                con.Open();

                // Make a random salt
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];

                //Fills array of bytes with a cryptographically strong sequence of random values.
                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();
                string passWithSalt = userPwInput + salt;
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passWithSalt));
                finalHash = Convert.ToBase64String(hashWithSalt);


                try
                {
                    string sql = "UPDATE User SET PasswordSalt = @PASSWORDSALT, Password = @PASSWORD WHERE Username=@USER;";
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@PASSWORDSALT", salt);
                        cmd.Parameters.AddWithValue("@PASSWORD", finalHash);
                        cmd.Parameters.AddWithValue("@USER", HttpContext.Session.GetString("user").ToString());
                        var update = cmd.ExecuteNonQuery();
                    }
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    // update pw last set timestamp

                    // No need void passwork token; for it is mandatory to change password this time.

                    string sql = "UPDATE User SET LastPwSet = @LASTPWSET WHERE Username=@USERID; ";
                    using (var cmdz = new MySqlCommand(sql, con))
                    {
                        cmdz.Parameters.AddWithValue("@LASTPWSET", DateTime.Now);
                        cmdz.Parameters.AddWithValue("@USERID", HttpContext.Session.GetString("user").ToString());
                        var update = cmdz.ExecuteNonQuery();
                    }

                    con.Close();
                }

                return RedirectToPage("/Index", false);

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
