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
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {


                string response = _svc.AddBlog(BlogModel);
                if (response == "True")
                {
                    ErrorMsg = "good";
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
