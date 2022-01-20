using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models.Blog;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Staff.Blog
{
    public class DeleteModel : PageModel
    {
        private readonly BlogService _svc;
        [BindProperty]
        public Post BlogModel { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        public DeleteModel(BlogService svc)
        {
            _svc = svc;
        }
        public IActionResult OnGet(int id)
        {
            _svc.RemovePost(id);
            return RedirectToPage("Dashboard");
        }
    }
}
