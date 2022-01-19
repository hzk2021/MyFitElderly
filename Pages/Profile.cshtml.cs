using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using NLog;

namespace EDP_Project.Pages
{
    public class ProfileModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        private static Logger logger = LogManager.GetLogger("MyAppLoggerRules");



        [BindProperty]

        public User myUser { get; set; }


        [BindProperty]


        public String inputPass { get; set; }


        [BindProperty]

        public List<Log> userAct { get; set; }

        [BindProperty]


        public String originalPath { get; set; }



        public User retrieverUserFromSession(string userID)
        {


            User theUser = new User();


            string sql = "select * FROM User WHERE Username = @USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader["PhotoPath"] != null)
                        {


                            if (reader["PhotoPath"] != DBNull.Value)
                            {
                                theUser.PhotoPath = reader["PhotoPath"].ToString();
                                //theUser.PhotoPath = @"~\Images\ff5eb79e-1d3e-42d9-a035-2150bd7bf6df_icon.jpg";
                            }


                        }


                        if (reader["Username"] != null)
                        {
                            if (reader["Username"] != DBNull.Value)
                            {
                                theUser.Username = reader["Username"].ToString();
                            }
                        }



                        if (reader["Email"] != null)
                        {
                            if (reader["Email"] != DBNull.Value)
                            {
                                theUser.Email = reader["Email"].ToString();
                            }
                        }


                        if (reader["EmailVerified"] != null)
                        {
                            if (reader["EmailVerified"] != DBNull.Value)
                            {
                                theUser.EmailVerified = reader["EmailVerified"].ToString();
                            }
                        }

                            




                        if (reader["Gender"] != null)
                        {
                            if (reader["Gender"] != DBNull.Value)
                            {
                                theUser.Gender = reader["Gender"].ToString();
                            }
                        }





                        if (reader["DateOfBirth"] != null)
                        {
                            if (reader["DateOfBirth"] != DBNull.Value)
                            {
                                theUser.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            }
                        }


                        if (reader["Contact"] != null)
                        {
                            if (reader["Contact"] != DBNull.Value)
                            {
                                theUser.Contact = reader["Contact"].ToString();
                            }
                        }




                        if (reader["Address"] != null)
                        {
                            if (reader["Address"] != DBNull.Value)
                            {
                                theUser.Address = reader["Address"].ToString();
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


            return theUser;


        }


        public void OnGet()
        {

            myUser = retrieverUserFromSession(HttpContext.Session.GetString("user"));



            userAct = new List<Log>();
            con.Open();
            string query2 = $"select * from log where `message` like '%{myUser.Username}%'";
            MySqlCommand cmd2 = new MySqlCommand(query2, con);
            //cmd2.Parameters.AddWithValue("@ELAINE", myUser.Username);
            MySqlDataReader dataReader2 = cmd2.ExecuteReader();

            try
            {
                //Create Command
                //Create a data reader and Execute the command

                //Read the data and store them in the list
                while (dataReader2.Read())
                {
                    userAct.Add(new Log
                    {

                        MachineName = dataReader2["MachineName"].ToString(),

                        Logged = (DateTime)dataReader2["Logged"],

                        Level = dataReader2["Level"].ToString(),

                        Message = dataReader2["Message"].ToString(),

                        Logger = dataReader2["Logger"].ToString(),


                        Properties = (dataReader2["Properties"] == DBNull.Value) ? string.Empty : dataReader2["Properties"].ToString(),


                        Callsite = dataReader2["Callsite"].ToString(),

                        Exception = dataReader2["Exception"].ToString(),


                    });
                }

            }
            catch (WebException ex)
            {
                throw ex;
            }
            finally
            {
                dataReader2.Close();
                con.Close();


            }

            originalPath = myUser.PhotoPath;



        }




        public IActionResult OnPostButton2(IFormCollection data)
        {

            myUser = retrieverUserFromSession(HttpContext.Session.GetString("user"));


            string salt;
            string finalHash;
            byte[] Key;
            byte[] IV;


            // swap shit here

            con.Open();




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
                string sql = "UPDATE User SET PasswordSalt = @PASSWORDSALT, Password = @PASSWORD WHERE Username=@USER; ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@PASSWORDSALT", salt);
                    cmd.Parameters.AddWithValue("@PASSWORD", finalHash);
                    cmd.Parameters.AddWithValue("@USER", myUser.Username);
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

            logger.Info($"{HttpContext.Session.GetString("user")} logged out");
            HttpContext.Session.Remove("user");
            HttpContext.Session.Clear();
            return RedirectToPage("Login", false);
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


        public IActionResult OnPost()
        {








            //  server side scrutiny

            var usernamePattern = "^[a-zA-Z][a-zA-Z ]{5,30}$";


            Match nameMatch = Regex.Match(myUser.Username, usernamePattern);




            var addressPat = "^[a-zA-Z][a-zA-Z ]{5,40}$";

            Match addMatch = Regex.Match(myUser.Address, addressPat);




            var contactPat = "^[0-9]{8}$";


            Match contactMatch = Regex.Match(myUser.Contact, contactPat);





            if (!nameMatch.Success)
            {
                ModelState.AddModelError("Username", "Username cannot contain special characters.");

            }

            if (!emailVerify(myUser.Email))
            {
                ModelState.AddModelError("Email", "Please enter a valid email.");

            }

            if (!contactMatch.Success)
            {
                ModelState.AddModelError("Username", "Please enter a valid contact number.");

            }



            if (!addMatch.Success)
            {
                ModelState.AddModelError("Address", "Please enter a valid address.");

            }




            // Given there's a photo submission...

                if (myUser.Photo != null)
                {



                string sqlz = "select PhotoPath FROM User WHERE Username = @USERID";
                MySqlCommand commandz = new MySqlCommand(sqlz, con);
                commandz.Parameters.AddWithValue("@USERID", HttpContext.Session.GetString("user"));
                try
                {
                    con.Open();
                    using (MySqlDataReader reader = commandz.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            if (reader["PhotoPath"] != null)
                            {


                                if (reader["PhotoPath"] != DBNull.Value)
                                {
                                    originalPath = reader["PhotoPath"].ToString();
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



                FileInfo file = new FileInfo(Path.Combine("wwwroot", originalPath));

                if (file.Exists)//check file exists
                {
                    //file.Delete();

                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();

                    System.IO.File.Delete(Path.Combine("wwwroot", originalPath));
                }



                string uploadsFolder = "wwwroot/Images";
                    string uniqueName = Guid.NewGuid().ToString() + "_" + myUser.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueName);

                    string photoPath = Path.Combine("Images", uniqueName);

                    myUser.Photo.CopyTo(new FileStream(filePath, FileMode.Create));












                    try
                    {
                        con.Open();

                        string sql = "UPDATE User SET Username = @USERNAME, Email = @EMAIL, Contact = @CONTACT, Address = @ADDRESS, PhotoPath = @PHOTOPATH WHERE Username =@USERID; ";
                        using (var cmd = new MySqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@USERNAME", myUser.Username);
                            cmd.Parameters.AddWithValue("@EMAIL", myUser.Email);
                            cmd.Parameters.AddWithValue("@CONTACT", myUser.Contact);
                            cmd.Parameters.AddWithValue("@ADDRESS", myUser.Address);
                            cmd.Parameters.AddWithValue("@USERID", HttpContext.Session.GetString("user"));
                            cmd.Parameters.AddWithValue("@PHOTOPATH", photoPath);
                            var update = cmd.ExecuteNonQuery();
                        }
                    }

                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        HttpContext.Session.SetString("user", myUser.Username);
                        con.Close();
                    }

                }

                else

                {


                    try
                    {
                        con.Open();

                        string sql = "UPDATE User SET Username = @USERNAME, Email = @EMAIL, Contact = @CONTACT, Address = @ADDRESS WHERE Username =@USERID; ";
                        using (var cmd = new MySqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@USERNAME", myUser.Username);
                            cmd.Parameters.AddWithValue("@EMAIL", myUser.Email);
                            cmd.Parameters.AddWithValue("@CONTACT", myUser.Contact);
                            cmd.Parameters.AddWithValue("@ADDRESS", myUser.Address);
                            cmd.Parameters.AddWithValue("@USERID", HttpContext.Session.GetString("user"));
                            var update = cmd.ExecuteNonQuery();
                        }
                    }

                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        HttpContext.Session.SetString("user", myUser.Username);
                        con.Close();
                    }

                }




                return RedirectToPage("/Profile");


            return Page();

        }


    }
}
