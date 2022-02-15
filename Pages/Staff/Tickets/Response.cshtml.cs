using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Tickets
{
    public class ResponseModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");



        [BindProperty]

        public Ticket myTicket { get; set; }

        [BindProperty]

        public string mailSubject { get; set; }

        [BindProperty]

        public string mailBody { get; set; }

        public void retrieveTicketInfo(string ticket_reference)
        {


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

        }
        public void OnGet()
        {

            myTicket = new Ticket();
            var ticket_reference_id = HttpContext.Request.Query["id"].ToString();
            retrieveTicketInfo(ticket_reference_id);

        }


        public IActionResult OnPost()
        {


            try
            {
                con.Open();
                string sql = "UPDATE Ticket SET Status = @STATUS WHERE Ticket_Reference_Id = @TICKET_ID; ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@STATUS", "Resolved");
                    cmd.Parameters.AddWithValue("@TICKET_ID", HttpContext.Request.Query["id"].ToString());
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



            // send email to ticket
            string to = "ktykuang@gmail.com"; //To address    
            string from = "contact.breadington.official@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);
            message.Subject = mailSubject;
            message.Body = "Hi " + myTicket.CustomerName + ", Thanks for contacting MyFitElderly!<br>" + mailBody;
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

            // Set ticket status to closed.







            return Redirect("/Staff/Tickets/Dashboard?responseSent=true");
        }


    }
}
