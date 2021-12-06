using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please enter your email")]

        public String Username { get; set; }

        [Required]

        public String Email { get; set; }
        [Required]
        public String Password { get; set; }

        [Required]

        public String Contact { get; set; }

        public String Role { get; set; }

    }
}
