using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace EDP_Project.Pages
{
    public class ContactModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        [BindProperty]

        public Ticket theTicket { get; set; }


        public class MyObject
        {
            public string success { get; set; }

            public List<String> ErrorMessage { get; set; }
        }
        public void OnGet()
        {
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


        public static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }

        public IActionResult OnPost()
        {


            // Generate ticket here.



            if (ValidateCaptcha())
            {

                try
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Ticket VALUES(NULL,  @ID, @EMAIL, @NAME, @CONTACT, @TYPE, @PROBLEM, @DATE, @STATUS)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", GenerateNewRandom());
                    cmd.Parameters.AddWithValue("@EMAIL", theTicket.CustomerEmail);
                    cmd.Parameters.AddWithValue("@NAME", theTicket.CustomerName);
                    cmd.Parameters.AddWithValue("@CONTACT", theTicket.CustomerContact);
                    cmd.Parameters.AddWithValue("@TYPE", "Email");
                    cmd.Parameters.AddWithValue("@PROBLEM", theTicket.Problem);
                    cmd.Parameters.AddWithValue("@DATE", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@STATUS", "Open");
                    var mother = cmd.ExecuteNonQuery();
                    con.Close();

                }

                catch (WebException ex)
                {
                    throw ex;
                }

                //return RedirectToPage("/Contact?querySent=true");
                return Redirect("/Contact?querySent=true");
            }

            else
            {
                return Page();
            }

        }


    }
}
