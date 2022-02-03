using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Accounts
{
    public class ListModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");






        private readonly UserService _userSvc;

        public ListModel(UserService userSvc)
        {
            _userSvc = userSvc;
        }



        [BindProperty]

        public List<User> userAccounts { get; set; }

        [BindProperty]

        public JsonResult nigger { get; set; }


        [BindProperty]

        public JsonResult niggerlogs { get; set; }

        [BindProperty]

        public int numActive { get; set; }


        [BindProperty]

        public int numInactive { get; set; }


        [BindProperty]

        public int numRegistrations { get; set; }

        [BindProperty]

        public List<User> newUsers { get; set; }


        [BindProperty]

        public List<Log> userLogs { get; set; }


        // =========== Chart Stuff =========== //

        [BindProperty]
        public int janGuest { get; set; }

        [BindProperty]
        public int febGuest { get; set; }

        [BindProperty]
        public int marGuest { get; set; }

        [BindProperty]
        public int aprGuest { get; set; }


        [BindProperty]
        public int mayGuest { get; set; }

        [BindProperty]
        public int junGuest { get; set; }

        [BindProperty]
        public int julGuest { get; set; }

        [BindProperty]
        public int augGuest { get; set; }

        [BindProperty]
        public int sepGuest { get; set; }

        [BindProperty]
        public int octGuest { get; set; }

        [BindProperty]
        public int novGuest { get; set; }

        [BindProperty]
        public int decGuest { get; set; }


        // =========== Chart Stuff =========== //


        [BindProperty]
        public int janStaff { get; set; }

        [BindProperty]
        public int febStaff { get; set; }

        [BindProperty]
        public int marStaff { get; set; }

        [BindProperty]
        public int aprStaff { get; set; }


        [BindProperty]
        public int mayStaff { get; set; }

        [BindProperty]
        public int junStaff { get; set; }

        [BindProperty]
        public int julStaff { get; set; }

        [BindProperty]
        public int augStaff { get; set; }

        [BindProperty]
        public int sepStaff { get; set; }

        [BindProperty]
        public int octStaff { get; set; }

        [BindProperty]
        public int novStaff { get; set; }

        [BindProperty]
        public int decStaff { get; set; }


        // =========== Chart Stuff =========== //






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




            userAccounts = new List<User>();

            newUsers = new List<User>();


            userLogs = new List<Log>();

            


            con.Open();

            string query = "SELECT * FROM User";

            MySqlCommand cmd = new MySqlCommand(query, con);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            try
            {
                //Create Command
                //Create a data reader and Execute the command

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    userAccounts.Add(new User
                    {

                        PhotoPath = dataReader["PhotoPath"].ToString(),

                        Username = dataReader["Username"].ToString(),

                        Email = dataReader["Email"].ToString(),

                        EmailVerified = dataReader["EmailVerified"].ToString(),

                        Token = (dataReader["Token"] == DBNull.Value) ? string.Empty : dataReader["Token"].ToString(),

                        TokenExpiry = (dataReader["TokenExpiry"] == DBNull.Value) ? default(DateTime) : (DateTime)dataReader["TokenExpiry"],

                        DateCreated = dataReader["DateCreated"].ToString(),

                        PasswordSalt = dataReader["PasswordSalt"].ToString(),

                        Password = dataReader["Password"].ToString(),

                        ResetPwToken = (dataReader["ResetPwToken"] == DBNull.Value) ? string.Empty : dataReader["ResetPwToken"].ToString(),

                        ResetPwTokenExpiry = (dataReader["ResetPwTokenExpiry"] == DBNull.Value) ? default(DateTime) : (DateTime)dataReader["ResetPwTokenExpiry"],

                        Contact = dataReader["Contact"].ToString(),

                        Status = dataReader["Status"].ToString(),

                        Role = dataReader["Role"].ToString(),

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


            string query2 = "SELECT * FROM Log";

            MySqlCommand cmd2 = new MySqlCommand(query2, con);

            MySqlDataReader dataReader2 = cmd2.ExecuteReader();

            try
            {
                //Create Command
                //Create a data reader and Execute the command

                //Read the data and store them in the list
                while (dataReader2.Read())
                {
                    userLogs.Add(new Log
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

            }

            var nigger = userLogs.Count();



            con.Close();





            //close Data Reader

            //close Connection


            newUsers = (List<User>)userAccounts.OrderBy(m => m.DateCreated).Take(8).ToList();



            foreach (User users in userAccounts)
            {

                string Country = "United States";


                // Loop through user list.

                // Turn country to short form

                // if country does not exist, add to list

                // else, increment the country




                var niggermannn = DateTime.Parse(users.DateCreated).Month;

                if ((DateTime.Now.Day).ToString() == users.DateCreated.Substring(4, 2)) numRegistrations += 1;


                if (users.Role.Trim() == "Guest")
                {

                    switch (DateTime.Parse(users.DateCreated).Month)
                    {

                        case 1:
                            janGuest += 1;
                            break;
                        case 2:
                            febGuest += 1;
                            break;
                        case 3:
                            marGuest += 1;
                            break;
                        case 4:
                            aprGuest += 1;
                            break;
                        case 5:
                            mayGuest += 1;
                            break;
                        case 6:
                            junGuest += 1;
                            break;
                        case 7:
                            julGuest += 1;
                            break;
                        case 8:
                            augGuest += 1;
                            break;
                        case 9:
                            sepGuest += 1;
                            break;
                        case 10:
                            octGuest += 1;
                            break;
                        case 11:
                            novGuest += 1;
                            break;
                        case 12:
                            decGuest += 1;
                            break;
                    }
                }

                else
                {


                    switch (DateTime.Parse(users.DateCreated).Month)
                    {

                        case 1:
                            janStaff += 1;
                            break;
                        case 2:
                            febStaff += 1;
                            break;
                        case 3:
                            marStaff += 1;
                            break;
                        case 4:
                            aprStaff += 1;
                            break;
                        case 5:
                            mayStaff += 1;
                            break;
                        case 6:
                            junStaff += 1;
                            break;
                        case 7:
                            julStaff += 1;
                            break;
                        case 8:
                            augStaff += 1;
                            break;
                        case 9:
                            sepStaff += 1;
                            break;
                        case 10:
                            octStaff += 1;
                            break;
                        case 11:
                            novStaff += 1;
                            break;
                        case 12:
                            decStaff += 1;
                            break;
                    }

                }


                numActive = userAccounts.Where(x => x.Status == "Active").Count();
                numInactive = userAccounts.Where(x => x.Status == "Inactive").Count();

            }
            return Page();

        }
    }
}


