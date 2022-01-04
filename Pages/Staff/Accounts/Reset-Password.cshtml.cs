using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Accounts
{
    public class Reset_PasswordModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        protected string GeneratePwToken(string userID)
        {

            con.Open();

            SHA512Managed hashing = new SHA512Managed();
            string pwToken = userID + DateTime.Now.ToString();
            byte[] hashedToken = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwToken));
            var hashedpwToken = Convert.ToBase64String(hashedToken);

            try
            {
                string sql = "UPDATE User SET ResetPwToken = @PWTOKEN, ResetPwTokenExpiry = @PWTOKENEXPIRY WHERE Username=@USERID; ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    cmd.Parameters.AddWithValue("@PWTOKEN", hashedpwToken);
                    cmd.Parameters.AddWithValue("@PWTOKENEXPIRY", DateTime.Now.AddMinutes(30));
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

            return hashedpwToken;

        }


        public void sendResetEmail(string userName)
        {

            var userEmail = "";

            // Do a query to get user's email


            string sql = "SELECT EMAIL FROM User WHERE Username=@USER";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USER", userName);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader["Email"] != DBNull.Value)
                        {
                            userEmail = reader["Email"].ToString();
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



            // send email

            string to = userEmail; //To address    
            string from = "contact.breadington.official@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);
            string mailbody = "<h1>Forgot your password?</h1><br> You requested a link to change your password. (If you didn't request this, you can ignore this email.)<br>reset your password by clicking on the link below <br>" + "https://localhost:44320/Auth/EndPasswordReset?token=" + HttpUtility.UrlEncode(GeneratePwToken(userName));
            message.Subject = "SITConnect Password Reset ✔ ";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("contact.breadington.official@gmail.com", "DawnInTheEast@Breadington");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.Redirect("/Staff/Accounts/List", false);
            }




        }


        public IActionResult OnGet(string username)
        {



            sendResetEmail(username);

            return RedirectToPage("/Staff/Accounts/List");




        }
    }
}
