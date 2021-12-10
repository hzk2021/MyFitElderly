using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {



            HttpContext.Session.Remove("user");
            HttpContext.Session.Clear();
            




            return RedirectToPage("Login", false);

        }
    }
}
