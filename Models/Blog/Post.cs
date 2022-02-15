using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models.Blog
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title cannot be left empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content cannot be left empty")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Header cannot be left empty")]
        public string Header { get; set; }
        [Required(ErrorMessage = "Category cannot be left empty")]
        public string Category { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;



    }
}
