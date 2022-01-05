using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Accounts
{
    public class DeleteModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        [BindProperty]

        public string userID { get; set; }


        public void deleteUser(string userId)
        {

            try
            {
                string MyConnection2 = "datasource=localhost;port=3307;username=root;password=root";
                string Query = "delete from user where Username='" + userID + "';";
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

            userID = HttpContext.Request.Query["id"];

            deleteUser(userID);

            return RedirectToPage("/Staff/Accounts/List");


        }
    }
}
