using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EDP_Project.Models.Blog;
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


        public void OnGet()
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
    }
}
