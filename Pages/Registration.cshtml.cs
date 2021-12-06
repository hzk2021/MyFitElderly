using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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


        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EDP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        public void createAccount(User user)
        {

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[User] VALUES(@Username, @Email, @PasswordSalt, @PasswordHash, @Contact, @Role)", con);
            con.Open();

            cmd.Parameters.AddWithValue("@Username", myUser.Username);
            cmd.Parameters.AddWithValue("@Email", myUser.Email);
            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
            cmd.Parameters.AddWithValue("@Contact", myUser.Contact);
            cmd.Parameters.AddWithValue("@Role", "Guest");


                var testing = cmd.ExecuteNonQuery();
            con.Close();

        }









        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
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




            




            return Page();
            }


    }
}
