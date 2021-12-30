using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using EDP_Project.Models;
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

            MySqlCommand cmd = new MySqlCommand("INSERT INTO User VALUES(NULL, @Username, @Email, @DateCreated, @PasswordSalt, @Password, @Contact, @Status, @Role)", con);
            con.Open();

            cmd.Parameters.AddWithValue("@Username", myUser.Username);
            cmd.Parameters.AddWithValue("@Email", myUser.Email);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
            cmd.Parameters.AddWithValue("@Password", finalHash);
            cmd.Parameters.AddWithValue("@Contact", myUser.Contact);
            cmd.Parameters.AddWithValue("@Status", "Active");
            cmd.Parameters.AddWithValue("@Role", "Guest");

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

        public IActionResult OnPost()
        {
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


                    createAccount(myUser);

                    return RedirectToPage("Login");

                    // On Post, Redirect back to LOGIN .

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
