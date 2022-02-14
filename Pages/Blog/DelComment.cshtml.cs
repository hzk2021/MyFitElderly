using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Blog
{
    public class DelCommentModel : PageModel
    {
        private readonly BlogService _svc;
        public DelCommentModel(BlogService svc)
        {
            _svc = svc;
        }
        //const string SessionCmtID = "_ID";
        //+HttpContext.Session.GetInt32(SessionCmtID)
        public IActionResult OnGet(int id)
        {
            
            _svc.RemoveComment(id);
            return RedirectToPage("/Blog/UserViewBlog");
        }
    }
}
