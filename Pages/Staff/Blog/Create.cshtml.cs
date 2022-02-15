using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models.Blog;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Blog
{
    public class CreateModel : PageModel
    {
        private readonly BlogService _svc;
        [BindProperty]
        public Post BlogModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public CreateModel(BlogService svc)
        {
            _svc = svc;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                string userRole = "Guest";
                MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");
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
                    return Page();
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
       // const string SessionAdddBlogSuccess = "addBlogcheck";
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                string response = _svc.AddBlog(BlogModel);

                if (response == "True")
                {
                    //HttpContext.Session.SetString(SessionAdddBlogSuccess, "good");
                    return RedirectToPage("Dashboard");
                }
                else
                {
                    ErrorMsg = response;
                }
            }
            return Page();

        }
    }
 }
