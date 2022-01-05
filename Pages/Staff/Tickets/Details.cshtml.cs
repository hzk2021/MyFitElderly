using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Tickets
{
    public class DetailsModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        [BindProperty]

        public Ticket myTicket { get; set; }


        public void retrieveTicketInfo(string ticket_reference)
        {


            // Decrypt String ID




            con.Open();


            MySqlCommand cmdx = new MySqlCommand("SELECT * FROM TICKET WHERE Ticket_Reference_Id = @TIXID", con);
            cmdx.Parameters.AddWithValue("@TIXID", ticket_reference);
            cmdx.ExecuteScalar();
            try
            {
                using (MySqlDataReader reader = cmdx.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Ticket_Reference_Id"] != null)
                        {
                            if (reader["Ticket_Reference_Id"] != DBNull.Value)
                            {

                                myTicket.Ticket_Reference_Id = reader["Ticket_Reference_Id"].ToString();
                            }

                        }

                        if (reader["CustomerEmail"] != null)
                        {
                            if (reader["CustomerEmail"] != DBNull.Value)
                            {

                                myTicket.CustomerEmail = (string)reader["CustomerEmail"];
                            }

                        }


                        if (reader["CustomerName"] != null)
                        {
                            if (reader["CustomerName"] != DBNull.Value)
                            {

                                myTicket.CustomerName = (string)reader["CustomerName"];
                            }

                        }

                        if (reader["CustomerContact"] != null)
                        {
                            if (reader["CustomerContact"] != DBNull.Value)
                            {

                                myTicket.CustomerContact = (string)reader["CustomerContact"];

                            }

                            if (reader["IssueType"] != null)
                            {
                                if (reader["IssueType"] != DBNull.Value)
                                {

                                    myTicket.IssueType = (string)reader["IssueType"];

                                }

                            }

                            if (reader["Problem"] != null)
                            {
                                if (reader["Problem"] != DBNull.Value)
                                {

                                    myTicket.Problem = (string)reader["Problem"];

                                }

                            }

                            if (reader["Date"] != null)
                            {
                                if (reader["Date"] != DBNull.Value)
                                {

                                    myTicket.DateCreated = (DateTime)reader["Date"];

                                }

                            }

                            if (reader["Status"] != null)
                            {
                                if (reader["Status"] != DBNull.Value)
                                {

                                    myTicket.Status= (string)reader["Status"];

                                }

                            }

                        }
                    }
                }
            }


            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }


        public void OnGet()
        {




            myTicket = new Ticket();


            var ticket_reference_id = HttpContext.Request.Query["id"].ToString();


            retrieveTicketInfo(ticket_reference_id);




        }
    }
}
