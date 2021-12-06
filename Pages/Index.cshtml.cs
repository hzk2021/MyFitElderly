using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]

        public string cUsername { get; set; }


        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("user") != null)
            {
                cUsername = HttpContext.Session.GetString("user");
                HttpContext.Session.Clear();
            }
            else
            {
                return RedirectToPage("Login");
            }

            return Page();

        }
    }
}
