using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Tickets
{
    public class DashboardModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        private readonly UserService _userSvc;

        public DashboardModel(UserService userSvc)
        {
            _userSvc = userSvc;
        }


            [BindProperty]

        public List<Ticket> allTickets { get; set; }

        [BindProperty]


        public int numOpen { get; set; }


        [BindProperty]


        public int numResolved { get; set; }


        [BindProperty]


        public int numClosed { get; set; }



        public IActionResult OnGet()
        {



            


            if (HttpContext.Session.GetString("user") != null)
            {

                var currentUser = HttpContext.Session.GetString("user").ToString();
                //HttpContext.Session.Clear();

                if (!_userSvc.isStaff(currentUser))
                {
                    return RedirectToPage("/Error/Error404");
                }
            }
            else
            {
                return Redirect("/Login");
            }


            allTickets = new List<Ticket>();


            con.Open();

            string query = "SELECT * FROM ticket";

            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            try
            {
                //Create Command
                //Create a data reader and Execute the command

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    allTickets.Add(new Ticket
                    {

                        Ticket_Reference_Id = dataReader["Ticket_Reference_Id"].ToString(),

                        CustomerEmail = dataReader["CustomerEmail"].ToString(),

                        CustomerName = dataReader["CustomerName"].ToString(),

                        CustomerContact = dataReader["CustomerContact"].ToString(),

                        IssueType = dataReader["IssueType"].ToString(),

                        Problem = dataReader["Problem"].ToString(),

                        DateCreated = (DateTime)dataReader["Date"],

                        Status = dataReader["Status"].ToString(),
                        
                    });
                }

            }
            catch (WebException ex)
            {
                throw ex;
            }

            finally
            {
                dataReader.Close();
            }



            numOpen = allTickets.Where(x => x.Status == "Open").Count();
            numResolved = allTickets.Where(x => x.Status == "Resolved").Count();
            numClosed = allTickets.Where(x => x.Status == "Closed").Count();


            return Page();

        }
    }
}
