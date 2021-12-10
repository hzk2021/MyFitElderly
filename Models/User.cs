using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Models
{

    [Keyless]
    public class User
    {
        [Required(ErrorMessage = "Please enter your email")]


        public String Username { get; set; }

        [Required]

        public String Email { get; set; }

        public String DateCreated { get; set; }


        public String PasswordSalt { get; set; }


        [Required]

        public String Password { get; set; }

        [Required]

        public String Contact { get; set; }

        public String Status {get; set;}

        public String Role { get; set; }

    }
}
