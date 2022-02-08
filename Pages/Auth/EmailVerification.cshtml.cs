using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages
{
    public class EmailVerificationModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");



        private readonly UserService _userSvc;

        public EmailVerificationModel(UserService userSvc)
        {
            _userSvc = userSvc;

        }


        public bool compareUserToken(string token)
        {

            DateTime expiry = new DateTime();

            con.Open();

            var actToken = HttpUtility.UrlDecode(token).Replace(" ", "+");



            MySqlCommand cmdx = new MySqlCommand("SELECT TOKENEXPIRY FROM USER WHERE TOKEN = @TOKEN", con);
            cmdx.Parameters.AddWithValue("@TOKEN", actToken);
            cmdx.ExecuteScalar();

            try
            {
                using (MySqlDataReader reader = cmdx.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["TokenExpiry"] != null)
                        {
                            if (reader["TokenExpiry"] != DBNull.Value)
                            {
                                expiry = (DateTime)reader["TokenExpiry"];
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }






            if (expiry.Subtract(DateTime.Now).TotalMinutes < 30)


            {

                //string h = null;
                //string sql = "select Token FROM User WHERE TOKEN = @USERTOKEN";

                try
                {




                    MySqlCommand cmd = new MySqlCommand("select * FROM User WHERE TOKEN = @USERTOKEN", con);
                    cmd.Parameters.AddWithValue("@USERTOKEN", actToken);
                    int tokenExists = Convert.ToInt32(cmd.ExecuteScalar());




                    if (tokenExists > 0)
                    {

                        // if exists, determiner whether link has expired.





                        // Update email verificaiton


                        try
                        {
                            string sql = "UPDATE User SET EMAILVERIFIED = @STATUS, TOKEN = @TOKEN, TOKENEXPIRY = @TOKENEXPIRY WHERE TOKEN=@USERTOKEN; ";
                            using (var cmd2 = new MySqlCommand(sql, con))
                            {
                                cmd2.Parameters.AddWithValue("@USERTOKEN", actToken);
                                cmd2.Parameters.AddWithValue("@STATUS", 1);
                                cmd2.Parameters.AddWithValue("@TOKEN", DBNull.Value);
                                cmd2.Parameters.AddWithValue("@TOKENEXPIRY", DBNull.Value);
                                var update = cmd2.ExecuteNonQuery();
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

                        return true;


                    }

                }

                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }




            }


            return false;

        }


        public IActionResult OnGet()
        {


            if (HttpContext.Session.GetString("user") != null)
            {


                if (HttpContext.Request.Query["token"].ToString() != "")
                {
                    if (compareUserToken(HttpContext.Request.Query["token"]))
                    {
                        return Redirect("~/");
                    }
                    else return RedirectToPage("VerificationFailed");

                }
            }
            else
            {
                return Redirect("/Login");
            }


            return Page();





        }

        public string GenerateNewToken()
        {
            con.Open();

            SHA512Managed hashing = new SHA512Managed();
            string token = HttpContext.Session.GetString("user").ToString() + DateTime.Now.ToString();
            byte[] hashedToken = hashing.ComputeHash(Encoding.UTF8.GetBytes(token));
            token = Convert.ToBase64String(hashedToken);
            try
            {
                string sql = "UPDATE User SET Token = @TOKEN, TokenExpiry = @TOKENEXPIRY WHERE Email=@USERID; ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@USERID", _userSvc.retrieveUserEmail(HttpContext.Session.GetString("user").ToString()));
                    cmd.Parameters.AddWithValue("@TOKEN", token);
                    cmd.Parameters.AddWithValue("@TOKENEXPIRY", DateTime.Now.AddMinutes(30));
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
            return token;
        }



        public IActionResult OnPost()
        {
            string to = "ktykuang@gmail.com"; //To address    
            string from = "contact.breadington.official@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "<b>Thank you for your registration! <br> please verify your account here:</b> https://localhost:44320/Auth/EmailVerification?token=" + GenerateNewToken();
            message.Subject = "MyFitElderly Account Registration ✔ ";
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


                return Redirect("/Auth/EmailVerification?responseSent=true");




        }


    }



}
