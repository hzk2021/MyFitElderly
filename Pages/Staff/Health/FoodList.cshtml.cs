using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages
{
    public class FoodListModel : PageModel
    {
        private readonly UserService _userSvc;
        public FoodListModel(UserService userSvc)
        {
            _userSvc = userSvc;
        }
        public void OnGet()
        {
            _userSvc.AIOCheckStaff();
        }
    }
}
