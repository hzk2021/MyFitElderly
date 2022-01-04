using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Accounts
{
    public class UpdateModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        [BindProperty]

        public User myUser { get; set; }

        [BindProperty]

        public string userID { get; set; }



        public void updateUserAccount(string userId)
        {

            if (myUser.Photo != null)
            {


                string uploadsFolder = "wwwroot/Images";
                string uniqueName = Guid.NewGuid().ToString() + "_" + myUser.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueName);

                string photoPath = Path.Combine("Images", uniqueName);

                myUser.Photo.CopyTo(new FileStream(filePath, FileMode.Create));


                try
                {
                    string MyConnection2 = "datasource=localhost;port=3307;username=root;password=root";
                    //string Query = "delete from user where Username='" + userID + "';";

                    string Query = "update user set PhotoPath= @PHOTOPATH , Username= @USERNAME, Gender= @GENDER, DateOfBirth= @DOB,   Email= @EMAIL ,Contact= @CONTACT, Address= @ADDRESS where username= @CUSER";

                    MySqlCommand MyCommand2 = new MySqlCommand(Query, con);

                    MyCommand2.Parameters.AddWithValue("@PHOTOPATH", photoPath);
                    MyCommand2.Parameters.AddWithValue("@USERNAME", myUser.Username);
                    MyCommand2.Parameters.AddWithValue("@GENDER", myUser.Gender);
                    MyCommand2.Parameters.AddWithValue("@DOB", myUser.DateOfBirth);

                    MyCommand2.Parameters.AddWithValue("@EMAIL", myUser.Email);
                    MyCommand2.Parameters.AddWithValue("@CONTACT", myUser.Contact);
                    MyCommand2.Parameters.AddWithValue("@ADDRESS", myUser.Address);
                    MyCommand2.Parameters.AddWithValue("@CUSER", userId);




                    MySqlDataReader MyReader2;
                    con.Open();
                    MyReader2 = MyCommand2.ExecuteReader();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                try
                {
                    //string Query = "delete from user where Username='" + userID + "';";

                    string Query = "update user set Username= @USERNAME, Gender= @GENDER, DateOfBirth= @DOB,   Email= @EMAIL ,Contact= @CONTACT, Address= @ADDRESS where username= @CUSER";

                    MySqlCommand MyCommand2 = new MySqlCommand(Query, con);

                    MyCommand2.Parameters.AddWithValue("@USERNAME", myUser.Username);
                    MyCommand2.Parameters.AddWithValue("@GENDER", myUser.Gender);
                    MyCommand2.Parameters.AddWithValue("@DOB", myUser.DateOfBirth);
                    MyCommand2.Parameters.AddWithValue("@EMAIL", myUser.Email);
                    MyCommand2.Parameters.AddWithValue("@CONTACT", myUser.Contact);
                    MyCommand2.Parameters.AddWithValue("@ADDRESS", myUser.Address);
                    MyCommand2.Parameters.AddWithValue("@CUSER", userId);
                    MySqlDataReader MyReader2;
                    con.Open();
                    MyReader2 = MyCommand2.ExecuteReader();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

            }

        }



        public void retrieveUserInfo(string userID)
        {


            // Decrypt String ID




            con.Open();


            MySqlCommand cmdx = new MySqlCommand("SELECT * FROM USER WHERE USERNAME = @USERID", con);
            cmdx.Parameters.AddWithValue("@USERID", userID);
            cmdx.ExecuteScalar();
            try
            {
                using (MySqlDataReader reader = cmdx.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PhotoPath"] != null)
                        {
                            if (reader["PhotoPath"] != DBNull.Value)
                            {

                                myUser.PhotoPath = reader["PhotoPath"].ToString();
                            }

                        }

                        if (reader["Username"] != null)
                        {
                            if (reader["Username"] != DBNull.Value)
                            {

                                myUser.Username = (string)reader["Username"];
                            }

                        }


                        if (reader["Gender"] != null)
                        {
                            if (reader["Gender"] != DBNull.Value)
                            {

                                myUser.Gender = (string)reader["Gender"];
                            }

                        }

                        if (reader["DateOfBirth"] != null)
                        {
                            if (reader["DateOfBirth"] != DBNull.Value)
                            {

                                myUser.DateOfBirth = (DateTime)reader["DateOfBirth"];

                            }

                            if (reader["DateOfBirth"] != null)
                            {
                                if (reader["DateOfBirth"] != DBNull.Value)
                                {

                                    myUser.DateOfBirth = (DateTime)reader["DateOfBirth"];

                                }

                            }

                            if (reader["Email"] != null)
                            {
                                if (reader["Email"] != DBNull.Value)
                                {

                                    myUser.Email = (string)reader["Email"];

                                }

                            }

                            if (reader["Contact"] != null)
                            {
                                if (reader["Contact"] != DBNull.Value)
                                {

                                    myUser.Contact = (string)reader["Contact"];

                                }

                            }

                            if (reader["Address"] != null)
                            {
                                if (reader["Address"] != DBNull.Value)
                                {

                                    myUser.Address = (string)reader["Address"];

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


            // Retrieve user information


            myUser = new User();

            retrieveUserInfo(HttpContext.Request.Query["id"].ToString());





        }


        public IActionResult OnPost()
        {
            userID = HttpContext.Request.Query["id"];

            updateUserAccount(userID);

            return RedirectToPage("/Staff/Accounts/List");
        }
    }
}
