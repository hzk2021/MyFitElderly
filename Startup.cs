using EDP_Project.Models.Survey;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Pract2.Models;
using Pract2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project
{

    public class Startup
    {
        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            con.Open();






            //  ------------------ On startup, if table already exist drop it , otherwise create new table ------------------


            //string lel_fml = @"DROP TABLE IF EXISTS user";
            //string dropSurveyTable = @"DROP TABLE IF EXISTS survey";


            //using (MySqlConnection conn = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password"))
            //{

            //    MySqlCommand cmd = new MySqlCommand(lel_fml, conn);
            //    MySqlCommand dst = new MySqlCommand(dropSurveyTable, conn);

            //    conn.Open();
            //    cmd.ExecuteNonQuery();

            //}


            MySqlCommand Create_table = new MySqlCommand(@"CREATE TABLE user (
            `Id`           INT            AUTO_INCREMENT  NOT NULL,
            `Username`     NCHAR (30)     NULL,
            `Email`        NVARCHAR (50)  NULL,
            `DateCreated`  NVARCHAR (50)  NULL,
            `PasswordSalt` LONGTEXT  NULL,
            `Password`     LONGTEXT  NULL,
            `Contact`      NVARCHAR (20)  NULL,
            `Status`       NVARCHAR (20)  DEFAULT ('Active') NULL,
            `Role`         NCHAR (10)     DEFAULT ('Guest') NULL,
            PRIMARY KEY (`Id` ASC)
            );", con);

            MySqlCommand create_surveyTable = new MySqlCommand(@"CREATE TABLE Survey (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `Category`          NCHAR(30)     NOT NULL,
            `Title`             NCHAR(100)    NOT NULL,
            `Description`       LONGTEXT      NULL,
            `CreatedOn`         DATETIME       NOT NULL,
            `UpdatedOn`         DATETIME       NOT NULL,
            `ViewStatus`        BIT DEFAULT ('Hide')  NOT NULL,
            `CreatedByStaffID`  INT            NOT NULL,
            PRIMARY KEY (`Id` ASC),
            FOREIGN KEY (`CreatedByStaffID`) REFERENCES user(Id)
            );", con);


            try
            {
                Create_table.ExecuteNonQuery();
                create_surveyTable.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

            //  ------------------ if table already exist drop it , otherwise create new table ------------------












        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddDbContext<UserDbContext>();

            services.AddTransient<UserService>();

            services.AddTransient<SurveyService>();

            services.AddRazorPages();

            //session timeout

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
