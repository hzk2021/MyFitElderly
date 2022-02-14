using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Health
{
    public class DeleteFoodRecordModel : PageModel
    {
        private readonly HealthService _svc;
        private readonly UserService _userSvc;

        [BindProperty]
        public string ErrorMsg { get; set; }

        public DeleteFoodRecordModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
        }

        public IActionResult OnGet(int userId)
        {
            _userSvc.AIOCheckGuest();

            // Ensures that page is not executed by other users on another person
            Console.WriteLine("Delete Food");
            int yourUserId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
            if (yourUserId == userId)
            {
                var response = _svc.RemoveMeals(userId);
                Console.WriteLine(response);

                if (response == "true")
                    return RedirectToPage("FoodJournal");
                else
                    ErrorMsg = response;

            }
            else
                ErrorMsg = "Error 403: Forbidden";

            Console.WriteLine("Reached here");
            return Page();
        }
    }
}
