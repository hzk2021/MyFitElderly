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

            //  ------------------ if table already exist drop it , otherwise create new table ------------------

            string lel_fml = @"DROP TABLE IF EXISTS log";


            using (MySqlConnection conn = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password"))
            {

                MySqlCommand cmd = new MySqlCommand(lel_fml, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

            }


            MySqlCommand Create_table = new MySqlCommand(@"CREATE TABLE Log (
            `Id`         INT AUTO_INCREMENT  NOT NULL,
            `UserID`     VARCHAR(500)  NOT NULL,
            `Date`       DATETIME(3)       NOT NULL,
            `Thread`     VARCHAR(255)  NOT NULL,
            `Level`      VARCHAR(50)   NOT NULL,
            `Logger`     VARCHAR(255)  NOT NULL,
            `Message`    VARCHAR(4000) NOT NULL,
            `IP_Address` VARCHAR(4000) NOT NULL,
            `Exception`  VARCHAR(2000) NULL,
            PRIMARY KEY(`Id` ASC)
            );", con);


            Create_table.ExecuteNonQuery();

            //  ------------------ if table already exist drop it , otherwise create new table ------------------












        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddDbContext<UserDbContext>();



            services.AddTransient<UserService>();

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
