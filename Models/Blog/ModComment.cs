using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models.Blog
{
    public class ModComment
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public int like { get; set; }
        public int dislike { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
