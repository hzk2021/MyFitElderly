using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using EDP_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;




namespace EDP_Project.Pages.Auth
{

    public class RegistrationModel : PageModel
    {



        static string salt;
        static string finalHash;
        byte[] Key;
        byte[] IV;


        [BindProperty]

        public User myUser { get; set; }




        public class MyObject
        {
            public string success { get; set; }

            public List<String> ErrorMessage { get; set; }
        }


        //SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EDP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        public void createAccount(User user)
        {

            //Photo.CopyTo("~/Images");

            /*vatarx.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);
*/

            string uploadsFolder = "wwwroot/Images";
            string uniqueName = Guid.NewGuid().ToString() + "_" + myUser.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueName);

            string photoPath = Path.Combine("Images", uniqueName);

            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();

            myUser.Photo.CopyTo(new FileStream(filePath, FileMode.Create));




            MySqlCommand cmd = new MySqlCommand("INSERT INTO User VALUES(NULL,  @PhotoPath, @Username, @Email, @EmailVerified, @Token, @TokenExpiry, @DateCreated, @PasswordSalt, @Password, @ResetPwToken, @ResetPwTokenExpiry, @Gender, @DateOfBirth, @Contact, @Status, @Role, @Address)", con);
            con.Open();

            cmd.Parameters.AddWithValue("@PhotoPath", photoPath);
            cmd.Parameters.AddWithValue("@Username", myUser.Username);
            cmd.Parameters.AddWithValue("@Email", myUser.Email);
            cmd.Parameters.AddWithValue("@EmailVerified", 0);
            cmd.Parameters.AddWithValue("@Token", DBNull.Value);
            cmd.Parameters.AddWithValue("@TokenExpiry", DBNull.Value);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
            cmd.Parameters.AddWithValue("@Password", finalHash);
            cmd.Parameters.AddWithValue("@ResetPwToken", DBNull.Value);
            cmd.Parameters.AddWithValue("@ResetPwTokenExpiry", DBNull.Value);
            cmd.Parameters.AddWithValue("@Gender", myUser.Gender);
            cmd.Parameters.AddWithValue("@DateOfBirth", myUser.DateOfBirth);
            cmd.Parameters.AddWithValue("@Contact", myUser.Contact);
            cmd.Parameters.AddWithValue("@Status", "Active");
            cmd.Parameters.AddWithValue("@Role", "Guest");
            cmd.Parameters.AddWithValue("@Address", myUser.Address);

            var mother = cmd.ExecuteNonQuery();


            con.Close();

        }





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



        public void OnGet()
        {
        }


        public string GenerateNewToken()
        {
            con.Open();

            SHA512Managed hashing = new SHA512Managed();
            string token = myUser.Username + DateTime.Now.ToString();
            byte[] hashedToken = hashing.ComputeHash(Encoding.UTF8.GetBytes(token));
            token = Convert.ToBase64String(hashedToken);

            try
            {
                string sql = "UPDATE User SET Token = @TOKEN, TokenExpiry = @TOKENEXPIRY WHERE Email=@USERID; ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@USERID", myUser.Email);
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


        public bool dateVerify(DateTime dob)
        {

            int userAge = DateTime.Today.Year - dob.Year;


            if (userAge < 13 || userAge > 99)
            {
                return false;

            }


            return true;

        }

        public bool emailVerify(string email)
        {


            try
            {
                email = new MailAddress(email).Address;
            }
            catch (FormatException)
            {
                return false;
            }

            return true;


        }



        public bool emailExists(string email)
        {


            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("select * FROM User WHERE Email = @EMAIL", con);
                cmd.Parameters.AddWithValue("@EMAIL", myUser.Email);
                int emailExistss = Convert.ToInt32(cmd.ExecuteScalar());


                if (emailExistss > 0)
                {
                    return true;
                }


            }
            catch (FormatException)
            {
                return true;
            }

            finally

            {
                con.Close();
            }
            


            return false;


        }






        public IActionResult OnPost()
        {
            //var usernamePattern = "^[a-zA-Z][a-zA-Z ]{5,30}$";// the whitelist


            //Match nameMatch = Regex.Match(myUser.Username, usernamePattern);




            //var addressPat = "^[a-zA-Z0-9][a-zA-Z0-9 ]{5,40}$";// the whitelist

            //Match addMatch = Regex.Match(myUser.Address, addressPat);


            //var pwPattern = @"^(?=[^A-Z\n]*[A-Z])(?=[^a-z\n]*[a-z])(?=[^0-9\n]*[0-9])(?=[^#?!@$%^&*\n-]*[#?!@$%^&*-]).{8,}$";



            //Match pwMatch = Regex.Match(myUser.Password, pwPattern);


            //if (myUser.Photo == null)
            //{
            //    ModelState.AddModelError("File", "Please upload a profile picture");
            //}



            //if (!nameMatch.Success)
            //{
            //    ModelState.AddModelError("Username", "Username cannot contain special characters");

            //}


            //if (!dateVerify(myUser.DateOfBirth))
            //{
            //    ModelState.AddModelError("DateOfBirth", "Please enter a valid date of birth.");

            //}


            if (!emailVerify(myUser.Email))
            {
                ModelState.AddModelError("Email", "Please enter a valid email.");

            }


            if (emailExists(myUser.Email))
            {
                ModelState.AddModelError("Email", "This email has already been registered.");

            }


            //if (!pwMatch.Success)
            //{
            //    ModelState.AddModelError("Password", "Password has to contain at least 8 characters, a number, lowercase, uppercase & special characters.");

            //}


            //if (!addMatch.Success)
            //{
            //    ModelState.AddModelError("Address", "Please enter a valid address.");

            //}












            if (ModelState.IsValid)
            {










                if (ValidateCaptcha())
                {






                    // Retrive password from user input
                    string password = myUser.Password.ToString().Trim();

                    // Make a random salt
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    byte[] saltByte = new byte[8];

                    //Fills array of bytes with a cryptographically strong sequence of random values.
                    rng.GetBytes(saltByte);
                    salt = Convert.ToBase64String(saltByte);

                    SHA512Managed hashing = new SHA512Managed();
                    string passWithSalt = password + salt;
                    byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(password));
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passWithSalt));
                    finalHash = Convert.ToBase64String(hashWithSalt);

                    RijndaelManaged cipher = new RijndaelManaged();
                    cipher.GenerateKey();
                    Key = cipher.Key;
                    IV = cipher.IV;





                    // Send email here.







                    createAccount(myUser);






                    //string to = "ktykuang@gmail.com"; //To address    
                    //string from = "contact.breadington.official@gmail.com"; //From address    
                    //MailMessage message = new MailMessage(from, to);

                    //string mailbody = "<b>Thank you for your registration! <br> please verify your account here:</b> https://localhost:44320/Auth/EmailVerification?token=" + GenerateNewToken();
                    //message.Subject = "MyFitElderly Account Registration ✔ ";
                    //message.Body = mailbody;
                    //message.BodyEncoding = Encoding.UTF8;
                    //message.IsBodyHtml = true;
                    //SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                    //System.Net.NetworkCredential basicCredential1 = new
                    //System.Net.NetworkCredential("contact.breadington.official@gmail.com", "DawnInTheEast@Breadington");
                    //client.EnableSsl = true;
                    //client.UseDefaultCredentials = false;
                    //client.Credentials = basicCredential1;
                    //try
                    //{
                    //    client.Send(message);
                    //}

                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //}

                    //finally
                    //{
                    //    Response.Redirect("Login", false);
                    //}




                    Response.Redirect("Login", false);



                    //return RedirectToPage("Login");

                    //On Post, Redirect back to LOGIN.

                    // Save crednetials to DB



                    // Create session

                    //HttpContext.Session.SetString("SSName", MyEmployee.Name);
                    //HttpContext.Session.SetString("SSDept", MyEmployee.Department.ToString());
                    //return RedirectToPage("Confirm");

                }




            }




            return Page();
        }


    }
}
