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

namespace EDP_Project.Pages.Blog
{
    public class BlogPostDetailModel : PageModel
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
        public List<ModComment> commentslist { get; set; }

        [BindProperty]
        public int pageNum { get; set; }

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public BlogPostDetailModel(BlogService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;

        }
        const string SessionCmtID = "_ID";
        public void OnGet(int id)
        {
            var userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
            BlogModel =  _svc.GetPostId(id);
            usrId = userId.ToString();
            pageNum = id;
            HttpContext.Session.SetInt32(SessionCmtID, id);

            commentslist = new List<ModComment>();
            con.Open();
            //string query = "SELECT Comments.comment, Comments.created, User.username From Comments FULL OUTER JOIN User ON Comments.id=User.id WHERE BlogId=" + id; 
            string query = "SELECT comments.Id, comments.Comment, comments.Created,comments.likey,comments.dislike, user.Username From comments JOIN user ON comments.userId = user.id WHERE BlogId=" + id;
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    commentslist.Add(new ModComment
                    {
                        Id = (int)dataReader["Id"],
                        Username = dataReader["Username"].ToString(),
                        Comment = dataReader["Comment"].ToString(),
                        Created = (DateTime)dataReader["Created"],
                        like = (int)(Convert.IsDBNull(dataReader["likey"]) ? null : (int?)dataReader["likey"]),
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
        }

        public IActionResult OnPost()
        {

                var userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
                //MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");
                //comment.UserId = userId;
                //comment.BlogId = 1;
                //comment.Created = DateTime.Now;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO comments VALUES(null, @UserId,@BlogId,@Comment,@like,@dislike,@Created)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BlogId", comment.BlogId);
                cmd.Parameters.AddWithValue("@Comment", comment.Comment);
                cmd.Parameters.AddWithValue("@like", 0);
                cmd.Parameters.AddWithValue("@dislike", 0);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                cmd.ExecuteNonQuery();
                //string response = _svc.AddComments(comment);

                con.Close();

                return RedirectToPage("BlogPostDetail");

        }
        //public IActionResult OnPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
        //        for (int i = 0; i < commentslist.Count; i++)
        //        {
        //            var svy_comments = commentslist[i];
        //            svy_comments.UserId = userId;
        //            svy_comments.BlogId = BlogModel.Id;
        //            svy_comments.Created = DateTime.Now;
        //            svy_comments.Comment = svy_comments.Comment;
        //        }
        //        string response = _svc.AddComments(commentslist);
        //    }
        //    return Page();
        //}

    }
}
