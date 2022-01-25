using Microsoft.AspNetCore.Http;
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

        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "Please upload a profile picture.")]

        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Please enter your username"), MinLength(5, ErrorMessage = "Username must be at least 5 characters long."), MaxLength(30, ErrorMessage = "Username cannot exceed 30 characters.")]

        public String Username { get; set; }

        [Required]

        public String Email { get; set; }
        public String EmailVerified { get; set; }

        public String Token { get; set; }

        public DateTime TokenExpiry { get; set; }



        public String DateCreated { get; set; }



        public String Password { get; set; }

        public String PasswordSalt { get; set; }

        public String ResetPwToken { get; set; }

        public DateTime ResetPwTokenExpiry { get; set; }

        public int FailedAttempts { get; set; }
        public DateTime LastLogin { get; set; }









        [Required]

        public String Gender { get; set; }

        [Required, DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }

        [Required, MinLength(8, ErrorMessage = "Number has to be at least 8 digits."), MaxLength(8)]


        public String Contact { get; set; }

        public String Status {get; set;}

        public String Role { get; set; }

        [Required, MinLength(5, ErrorMessage = "Address has to be at least 5 characters long."), MaxLength(40, ErrorMessage = "Address has to be less than 40 characters.")]

        public String Address { get; set; }

    }
}
