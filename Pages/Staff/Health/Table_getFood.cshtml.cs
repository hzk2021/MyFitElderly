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
    public class Table_getFoodModel : PageModel
    {
        private readonly HealthService _svc;
        public Table_getFoodModel(HealthService svc)
        {
            _svc = svc;
        }
        public JsonResult OnGet(string search, string sort, string order, int limit, int offset)
        {
            object records = _svc.GetFoodRecords(search, sort, order, limit, offset);
            return new JsonResult(records);
        }
    }
}
