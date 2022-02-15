using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EDP_Project.Models.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Blog
{
    public class DashboardModel : PageModel
    {
        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");
        [BindProperty]
        public List<Post> BlogPosts { get; set; }


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                string userRole = "Guest";
                string sql = "SELECT * FROM User WHERE Username=@USER";
                MySqlCommand command = new MySqlCommand(sql, con);
                command.Parameters.AddWithValue("@USER", HttpContext.Session.GetString("user"));
                try
                {
                    con.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            if (reader["Role"] != DBNull.Value)
                            {
                                userRole = reader["Role"].ToString();
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
                if (userRole == "Staff")
                {
                    BlogPosts = new List<Post>();
                    con.Open();

                    string query = "SELECT * FROM Post";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    try
                    {
                        //Create Command
                        //Create a data reader and Execute the command

                        //Read the data and store them in the list
                        while (dataReader.Read())
                        {
                            BlogPosts.Add(new Post
                            {
                                Id = (int)dataReader["Id"],
                                Title = dataReader["Title"].ToString(),
                                Category = dataReader["Category"].ToString(),
                                Header = dataReader["Header"].ToString(),
                                Content = dataReader["Content"].ToString(),
                                Created = (DateTime)dataReader["Created"],


                            });
                        }


                        return Page();
                    }
                    catch (WebException ex)
                    {
                        throw ex;
                    }

                    finally
                    {
                        dataReader.Close();
                    }
                }
                else
                {
                    return RedirectToPage("/Error/Error404");
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
    }
}
