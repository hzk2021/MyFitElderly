using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NLog;

namespace EDP_Project.Pages
{
    public class LogoutModel : PageModel
    {

        private static Logger logger = LogManager.GetLogger("MyAppLoggerRules");

        public IActionResult OnGet()
        {



            logger.Info($"{HttpContext.Session.GetString("user")} logged out");
            HttpContext.Session.Remove("user");
            HttpContext.Session.Clear();
            return RedirectToPage("Login", false);

        }



    }
}
