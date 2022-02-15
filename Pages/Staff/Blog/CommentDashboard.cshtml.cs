using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EDP_Project.Models.Blog;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages.Staff.Blog
{
    public class CommentDashboardModel : PageModel
    {
        private readonly BlogService _svc;
        private readonly UserService _userSvc;
        [BindProperty]
        public Post BlogModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        [BindProperty]
        public string usrId { get; set; }

        [BindProperty]
        public DateTime Date { get; set; }

        [BindProperty]
        public Comments comment { get; set; }

        [BindProperty]
        public List<Comments> commentslist { get; set; }

        [BindProperty]
        public int pageNum { get; set; }

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public CommentDashboardModel(BlogService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;

        }
        const string SessionCmtID = "_ID";
        public IActionResult OnGet(int id)
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
                    var userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
                    usrId = userId.ToString();
                    pageNum = id;
                    HttpContext.Session.SetInt32(SessionCmtID, id);

                    commentslist = new List<Comments>();
                    con.Open();
                    string query = "SELECT * FROM Comments";
                    //string query = "SELECT comments.Id, comments.Comment, comments.Created,comments.likey,comments.dislike, user.Username From comments JOIN user ON comments.userId = user.id WHERE BlogId=" + id;
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    try
                    {
                        while (dataReader.Read())
                        {
                            commentslist.Add(new Comments
                            {
                                Id = (int)dataReader["Id"],
                                UserId = (int)dataReader["UserId"],
                                BlogId = (int)dataReader["BlogId"],
                                Comment = dataReader["Comment"].ToString(),
                                Created = (DateTime)dataReader["Created"],
                                likey = (int)(Convert.IsDBNull(dataReader["likey"]) ? null : (int?)dataReader["likey"]),
                                dislike = (int)(Convert.IsDBNull(dataReader["dislike"]) ? null : (int?)dataReader["dislike"])
                            }); ; ; ;
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
    }
}
