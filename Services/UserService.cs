using Microsoft.EntityFrameworkCore;
using EDP_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pract2.Models;

namespace EDP_Project.Services
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
