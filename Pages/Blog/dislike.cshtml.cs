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

namespace EDP_Project.Pages.Blog
{
    public class dislikeModel : PageModel
    {
        private readonly BlogService _svc;
        private readonly UserService _userSvc;
        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public dislikeModel(BlogService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;

        }
        [BindProperty]
        public List<likeDisCmt> dislikecntlist { get; set; }
        public string usrId { get; set; }
        public IActionResult OnGet(int id)
        {
            var userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
            dislikecntlist = new List<likeDisCmt>();
            con.Open();

            string queryCmtID = "SELECT Id,likey,dislike from comments where Id=" + id;
            MySqlCommand cmd = new MySqlCommand(queryCmtID, con);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                dislikecntlist.Add(new likeDisCmt
                {
                    Id = (int)dataReader["Id"],
                    like = (int)(Convert.IsDBNull(dataReader["likey"]) ? null : (int?)dataReader["likey"]),
                    dislike = (int)(Convert.IsDBNull(dataReader["dislike"]) ? null : (int?)dataReader["dislike"])
                }); ; ; ;
            }
            dataReader.Close();
            int dislikesum = dislikecntlist[0].dislike + 1;
            string queryCmtID3 = $"SET SQL_SAFE_UPDATES = 0;UPDATE comments SET dislike = {dislikesum} WHERE Id =" + id;
            MySqlCommand cmd3 = new MySqlCommand(queryCmtID3, con);
            cmd3.ExecuteNonQuery();
            return RedirectToPage($"/Blog/UserViewBlog");

        }
    }
}
