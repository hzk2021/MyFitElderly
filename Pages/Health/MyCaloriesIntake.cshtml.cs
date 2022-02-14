using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.Health
{
    public class MyCaloriesIntakeModel : PageModel
    {
        private readonly HealthService _svc;
        private readonly UserService _userSvc;

        [BindProperty]
        public List<CaloriesIntakes> ChartRecords { get; set; }
        public MyCaloriesIntakeModel(HealthService svc, UserService userSvc)
        {
            _svc = svc;
            _userSvc = userSvc;
        }

        public void OnGet()
        {
            int userId = _userSvc.retrieveuserid(HttpContext.Session.GetString("user"));
            ChartRecords = _svc.GetChartCaloriesIntake(userId);
        }
    }
}
