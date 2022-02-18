using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models.Blog
{
    public class Comments
    {   [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string Comment { get; set; }
        public int likey { get; set; }
        public int dislike { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;


    }
}
