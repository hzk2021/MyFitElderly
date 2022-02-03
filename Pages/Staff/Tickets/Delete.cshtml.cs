using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Tickets
{
    public class DeleteModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public void deleteTicket(string ticketID)
        {

            try
            {
                string Query = "delete from ticket where Ticket_Reference_Id='" + ticketID + "';";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, con);
                MySqlDataReader MyReader2;
                con.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                while (MyReader2.Read())
                {
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }




        }



        public IActionResult OnGet()
        {

            var ticketID = HttpContext.Request.Query["id"];

            deleteTicket(ticketID);

            return RedirectToPage("/Staff/Tickets/Dashboard");


        }
    }
}
