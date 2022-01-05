using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages
{
    public class ContactModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        [BindProperty]

        public Ticket theTicket { get; set; }

        public void OnGet()
        {
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




            // send email







            //return RedirectToPage("/Contact?querySent=true");
            return Redirect("/Contact?querySent=true");

        }


    }
}
