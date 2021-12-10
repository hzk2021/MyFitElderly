using EDP_Project.Models;
using Microsoft.EntityFrameworkCore;
using Pract2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pract2.Services
{
    public class UserService
    {
        private readonly UserDbContext _context;


        public UserService(UserDbContext context)
        {
            _context = context;
        }



        public List<User> GetAllUsers()
        {
            List<User> AllUsers = new List<User>();
            AllUsers = _context.User.ToList();
            return AllUsers;

        }

   

    }



    



}
