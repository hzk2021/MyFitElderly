using EDP_Project.Services;
using EDP_Project.Services.Survey;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Pract2.Models;
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




            ////  ------------------ On startup, if table already exist drop it , otherwise create new table ------------------


            //string lel_fml = @"DROP TABLE IF EXISTS user";
            //string dropLogTable = @"DROP TABLE IF EXISTS log";
            //string dropSurveyTable = @"DROP TABLE IF EXISTS survey";
            //string dropQuestionTable = @"DROP TABLE IF EXISTS question";
            //string dropQuestionOptionTable = @"DROP TABLE IF EXISTS questionoption";

            //string dropCaloriesIntake = @"DROP TABLE IF EXISTS caloriesintake";
            //string dropMeals = @"DROP TABLE IF EXISTS meals";


            //using (MySqlConnection conn = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password"))
            //{

            //    MySqlCommand cmd = new MySqlCommand(lel_fml, conn);
            //    MySqlCommand dlt = new MySqlCommand(dropLogTable, conn);
            //    MySqlCommand dst = new MySqlCommand(dropSurveyTable, conn);
            //    MySqlCommand dqt = new MySqlCommand(dropQuestionTable, conn);
            //    MySqlCommand dqot = new MySqlCommand(dropQuestionOptionTable, conn);

            //    MySqlCommand dci = new MySqlCommand(dropCaloriesIntake, conn);
            //    MySqlCommand dm = new MySqlCommand(dropMeals, conn);

            //    conn.Open();
            //    dlt.ExecuteNonQuery();
            //    dm.ExecuteNonQuery();
            //    dci.ExecuteNonQuery();
            //    dqot.ExecuteNonQuery();
            //    dqt.ExecuteNonQuery();
            //    dst.ExecuteNonQuery();
            //    cmd.ExecuteNonQuery();


            //}


            MySqlCommand create_log_table = new MySqlCommand(@"
CREATE TABLE Log (
   `ID` int AUTO_INCREMENT NOT NULL,
   `MachineName` nvarchar(200) NULL,
   `Logged` datetime NOT NULL,
   `Level` varchar(5) NOT NULL,
   `Message` Longtext NOT NULL,
   `Logger` nvarchar(300) NULL,
   `Properties` Longtext NULL,
   `Callsite` nvarchar(300) NULL,
   `Exception` Longtext NULL,
	PRIMARY KEY (`ID` ASC) 
);", con);


            MySqlCommand Create_table = new MySqlCommand(@"CREATE TABLE `user` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PhotoPath` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Username` char(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Email` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `EmailVerified` tinyint(1) DEFAULT NULL,
  `Token` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `TokenExpiry` datetime DEFAULT NULL,
  `DateCreated` date DEFAULT NULL,
  `PasswordSalt` longtext,
  `Password` longtext,
  `ResetPwToken` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ResetPwTokenExpiry` datetime DEFAULT NULL,
  `Gender` char(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `DateOfBirth` datetime DEFAULT NULL,
  `Contact` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Status` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT (_utf8mb4'Active'),
  `Role` char(10) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT (_utf8mb4'Guest'),
  `Address` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;", con);



            MySqlCommand Create_caloriesIntake = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS caloriesIntake (
            `Date`              DATETIME        NOT NULL,
            `UserId`                INT             NOT NULL,
            `Day`               NVARCHAR (15)   NOT NULL,
            `CaloriesIntake`    INT             NULL,
            PRIMARY KEY (`Date` ASC),
            FOREIGN KEY (`UserId`) REFERENCES user(`Id`)
            );", con);

            MySqlCommand Create_foodList = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS food (
            `FoodId`       INT              AUTO_INCREMENT  NOT NULL,
            `FoodName`     NVARCHAR (50)    NOT NULL,
            `Category`     NVARCHAR (20)    NULL,
            `Calories`     INT              NOT NULL,
            PRIMARY KEY (`FoodId` ASC)
            );", con);

            MySqlCommand Create_mealItems = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS meals (
            `ItemId`       INT            AUTO_INCREMENT  NOT NULL,
            `FoodId`       INT            NOT NULL,
            `MealType`     NVARCHAR (20)  NOT NULL,
            `Date`         DATETIME       NULL,
            PRIMARY KEY (`ItemId` ASC),
            FOREIGN KEY (`FoodId`) REFERENCES food(`FoodId`),
            FOREIGN KEY (`Date`) REFERENCES caloriesIntake(`Date`)
            );", con);

            MySqlCommand create_surveyTable = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS Survey (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `SurveyUUID`        CHAR(36)      NOT NULL,
            `Category`          NCHAR(30)     NOT NULL,
            `Title`             NCHAR(100)    NOT NULL,
            `Description`       LONGTEXT      NULL,
            `CreatedOn`         DATETIME       NOT NULL,
            `UpdatedOn`         DATETIME       NOT NULL,
            `ViewStatus`        BIT            NOT NULL,
            `CreatedByStaffID`  INT            NOT NULL,
            UNIQUE (SurveyUUID),
            PRIMARY KEY (`Id` ASC),
            FOREIGN KEY (`CreatedByStaffID`) REFERENCES user(Id)
            );", con);

            MySqlCommand create_questionTable = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS Question (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `QuestionUUID`      CHAR(36)       NOT NULL,
            `Text`              NCHAR(255)     NOT NULL,
            `BelongsToSurveyID` CHAR(36)      NOT NULL,
            PRIMARY KEY (`Id` ASC),
            UNIQUE (QuestionUUID),
            FOREIGN KEY (`BelongsToSurveyID`) REFERENCES survey(SurveyUUID)
            );", con);

            MySqlCommand create_questionOptionTable = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS QuestionOption (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `OptionUUID`      CHAR(36)       NOT NULL,
            `Text`              NCHAR(255)     NOT NULL,
            `BelongsToQuestionID` CHAR(36)      NOT NULL,
            PRIMARY KEY (`Id` ASC),
            UNIQUE (OptionUUID),
            FOREIGN KEY (`BelongsToQuestionID`) REFERENCES question(QuestionUUID)
            );", con);


            MySqlCommand Create_Blog = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS blog (
            `Id`       INT              AUTO_INCREMENT  NOT NULL,
            `Title`     NVARCHAR (50)    NOT NULL,
            `Content`     NVARCHAR (50)    NULL,
            PRIMARY KEY (`Id` ASC),
            FOREIGN KEY (`User_ID`) REFERENCES user(Id)
            );", con);

            //try
            //{
            //    Create_table.ExecuteNonQuery();
            //    create_log_table.ExecuteNonQuery();
            //    create_surveyTable.ExecuteNonQuery();
            //    create_questionTable.ExecuteNonQuery();
            //    create_questionOptionTable.ExecuteNonQuery();
            //    Create_caloriesIntake.ExecuteNonQuery();
            //    Create_foodList.ExecuteNonQuery();
            //    Create_mealItems.ExecuteNonQuery();
            //}
            //catch (Exception)
            //{

            //}









        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddDbContext<UserDbContext>();

            services.AddTransient<UserService>();
            services.AddTransient<HealthService>();

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
