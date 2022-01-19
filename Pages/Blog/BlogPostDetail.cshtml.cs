using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models.Blog;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Blog
{
    public class BlogPostDetailModel : PageModel
    {
        private readonly BlogService _svc;
        [BindProperty]
        public Post BlogModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }
        public BlogPostDetailModel(BlogService svc)
        {
            _svc = svc;
        }
        public void OnGet(int id)
        {
            BlogModel = _svc.GetPostId(id);
        }

    }
}
