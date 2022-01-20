using EDP_Project.Models;
using EDP_Project.Models.Blog;
using EDP_Project.Models.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pract2.Models
{
    public class UserDbContext : DbContext
    {

        // Inject Iconfiguration to access appsettings.json
        private readonly IConfiguration _config;
        public UserDbContext(IConfiguration configuration)
        {
            _config = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get connection string from the value of "MyConn" in appsetting and
            // Configure context to connect to microsoft SQL server database    
            string connectionString = _config.GetConnectionString("MySQLCon");
            optionsBuilder.UseMySQL(connectionString);

        }

        // Map employee entity to Employees table in database
        //public DbSet<User> User { get; set; }
        public DbSet<Survey> Survey { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<CaloriesIntakes> CaloriesIntakes { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Meals> Meals { get; set; }

        public DbSet<QuestionOption> QuestionOption { get; set; }
        public DbSet<Post> Post { get; set; }

        public DbSet<SurveyResponse> SurveyResponse { get; set; }
    }
}
