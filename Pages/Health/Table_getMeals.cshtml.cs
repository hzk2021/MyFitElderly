using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EDP_Project.Models;

namespace EDP_Project.Pages.Health
{
    public class Table_getMealsModel : PageModel
    {
        private readonly HealthService _svc;
        private readonly UserService _userSvc;
        public Table_getMealsModel(HealthService svc, UserService userSvc)
        {
            _userSvc = userSvc;
            _svc = svc;
        }
        public JsonResult OnGet(string search, string sort, string order, int limit, int offset)
        {
            int userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));

            Console.WriteLine(userId.ToString()+ " , " +search + " , " + sort + " , " + order + " , " + limit + " , " + offset);
            object records = _svc.GetMealsRecord(userId, search, sort, order, limit, offset);
            return new JsonResult(records);
        }
    }
}
