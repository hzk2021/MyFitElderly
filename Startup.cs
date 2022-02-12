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


            string lel_fml = @"DROP TABLE IF EXISTS user";
            string dropLogTable = @"DROP TABLE IF EXISTS log";
            string dropSurveyTable = @"DROP TABLE IF EXISTS survey";
            string dropQuestionTable = @"DROP TABLE IF EXISTS question";
            string dropQuestionOptionTable = @"DROP TABLE IF EXISTS questionoption";
            string dropSurveyResponseTable = @"DROP TABLE IF EXISTS surveyresponse";
            string dropExerciseRoutineTable = @"DROP TABLE IF EXISTS exerciseroutines";

            string dropCaloriesIntake = @"DROP TABLE IF EXISTS caloriesintake";
            string dropMeals = @"DROP TABLE IF EXISTS meals";

            string dropComments = @"DROP TABLE IF EXISTS comments";


            using (MySqlConnection conn = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password"))
            {

               // MySqlCommand cmd = new MySqlCommand(lel_fml, conn);
                MySqlCommand dlt = new MySqlCommand(dropLogTable, conn);
                MySqlCommand dst = new MySqlCommand(dropSurveyTable, conn);
                MySqlCommand dqt = new MySqlCommand(dropQuestionTable, conn);
                MySqlCommand dqot = new MySqlCommand(dropQuestionOptionTable, conn);

                MySqlCommand dci = new MySqlCommand(dropCaloriesIntake, conn);
                MySqlCommand dm = new MySqlCommand(dropMeals, conn);

                MySqlCommand dsrt = new MySqlCommand(dropSurveyResponseTable, conn);
                MySqlCommand dert = new MySqlCommand(dropExerciseRoutineTable, conn);

               // MySqlCommand dctt = new MySqlCommand(dropComments, conn);

                conn.Open();
                dlt.ExecuteNonQuery();
                dm.ExecuteNonQuery();
                dci.ExecuteNonQuery();
                dqot.ExecuteNonQuery();
                dqt.ExecuteNonQuery();
                dert.ExecuteNonQuery();
                dsrt.ExecuteNonQuery();
                dst.ExecuteNonQuery();
                //dctt.ExecuteNonQuery();
               // cmd.ExecuteNonQuery();


            }


            MySqlCommand create_log_table = new MySqlCommand(@"
CREATE TABLE IF NOT EXISTS Log (
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


            MySqlCommand Create_table = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS `user` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PhotoPath` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Username` char(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Email` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `EmailVerified` tinyint(1) DEFAULT NULL,
  `TwoFactorVerified` tinyint(1) DEFAULT NULL,
  `Token` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `TokenExpiry` datetime DEFAULT NULL,
  `DateCreated` date DEFAULT NULL,
  `PasswordSalt` longtext,
  `Password` longtext,
  `ResetPwToken` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ResetPwTokenExpiry` datetime DEFAULT NULL,

  `FailedAttempts` INT DEFAULT 0,
  `LastFailed` datetime DEFAULT NULL,
  `LastPwSet` datetime DEFAULT NULL,

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
            `UserId`            INT             NOT NULL,
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
            `Id`           INT            AUTO_INCREMENT  NOT NULL,
            `UserId`       INT            NOT NULL,
            `FoodId`       INT            NOT NULL,
            `MealType`     NVARCHAR (20)  NOT NULL,
            `Quantity`     INT            NOT NULL,
            `Date`         DATETIME       NOT NULL,
            PRIMARY KEY (`Id` ASC),
            FOREIGN KEY (`FoodId`) REFERENCES food(`FoodId`),
            FOREIGN KEY (`UserId`) REFERENCES user(`Id`)
            );", con);

            MySqlCommand Create_exerciseList = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS exercise (
            `ExerciseId`            INT              AUTO_INCREMENT  NOT NULL,
            `ExerciseName`          NVARCHAR (50)    NOT NULL,
            `Measurement`           NVARCHAR (20)    NOT NULL,
            `CaloriesBurnPerUnit`   INT              NOT NULL,
            PRIMARY KEY (`ExerciseId` ASC)
            );", con);

            MySqlCommand Create_exerciseRoutines = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS exerciseRoutines (
            `RoutineId`             INT              AUTO_INCREMENT  NOT NULL,
            `UserId`                INT              NOT NULL,
            `ExerciseId`            INT              NOT NULL,
            `Intensity`             DOUBLE           NOT NULL,
            `Day`                   NVARCHAR(15)     NOT NULL,
            PRIMARY KEY (`RoutineId` ASC),
            FOREIGN KEY (`ExerciseId`) REFERENCES exercise(`ExerciseId`),
            FOREIGN KEY (`UserId`) REFERENCES user(`Id`)
            );", con);

            MySqlCommand create_surveyTable = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS Survey (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `SurveyUUID`        CHAR(36)      NOT NULL,
            `Category`          NCHAR(30)     NOT NULL,
            `Title`             NCHAR(100)    NOT NULL,
            `Description`       LONGTEXT      NULL,
            `CreatedOn`         DATETIME       NOT NULL,
            `UpdatedOn`         DATETIME       NOT NULL,
            `ViewStatus`        NCHAR(30)            NOT NULL,
            `CreatedByStaffID`  INT            NOT NULL,
            `ImgBytes`         MEDIUMBLOB   NULL,
            UNIQUE (SurveyUUID),
            PRIMARY KEY (`Id` ASC),
            FOREIGN KEY (`CreatedByStaffID`) REFERENCES user(Id) ON DELETE CASCADE
            );", con);

            MySqlCommand create_questionTable = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS Question (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `QuestionUUID`      CHAR(36)       NOT NULL,
            `Text`              NCHAR(255)     NOT NULL,
            `BelongsToSurveyID` CHAR(36)      NOT NULL,
            PRIMARY KEY (`Id` ASC),
            UNIQUE (QuestionUUID),
            FOREIGN KEY (`BelongsToSurveyID`) REFERENCES survey(SurveyUUID) ON DELETE CASCADE
            );", con);

            MySqlCommand create_questionOptionTable = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS QuestionOption (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `OptionUUID`      CHAR(36)       NOT NULL,
            `Text`              NCHAR(255)     NOT NULL,
            `BelongsToQuestionID` CHAR(36)      NOT NULL,
            PRIMARY KEY (`Id` ASC),
            UNIQUE (OptionUUID),
            FOREIGN KEY (`BelongsToQuestionID`) REFERENCES question(QuestionUUID) ON DELETE CASCADE
            );", con);

            MySqlCommand create_surveyResponseTable = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS SurveyResponse (
            `Id`                INT            AUTO_INCREMENT  NOT NULL,
            `SurveyResponseUUID`      CHAR(36)       NOT NULL,
            `Question_Text`      NCHAR(255)       NOT NULL,
            `Response_Text`              NCHAR(255)     NOT NULL,
            `ReferenceToSurveyID`      CHAR(36)       NOT NULL,
            `SubmittedByCustomerID`      INT       NOT NULL,
            PRIMARY KEY (`Id` ASC),
            UNIQUE (SurveyResponseUUID),
            FOREIGN KEY (`ReferenceToSurveyID`) REFERENCES survey(SurveyUUID) ON DELETE CASCADE,
            FOREIGN KEY (`SubmittedByCustomerID`) REFERENCES user(Id) ON DELETE CASCADE
            );", con);


            MySqlCommand Create_Blog = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS post (
            `Id`        INT              AUTO_INCREMENT  NOT NULL,
            `Title`     NVARCHAR (50)    NOT NULL,
            `Header`    NVARCHAR (500)   NULL,
            `Content`   NVARCHAR (4000)  NULL,
            `Category`  NVARCHAR (50)    NULL,
            `Created`   DATETIME         NOT NULL,
            PRIMARY KEY (`Id` ASC)

            );", con);
            MySqlCommand Create_comments = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS comments (
            `Id`          INT              AUTO_INCREMENT  NOT NULL,
            `UserId`      INT              NOT NULL,
            `BlogId`      INT              NOT NULL,
            `Comment`     NVARCHAR (4000)  NOT NULL,
            `Created`     DATETIME         NOT NULL,
            PRIMARY KEY (`Id` ASC)
            );", con);




            MySqlCommand Create_specialistDepartment = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS specialistDepartment (
            `Id`            INT             AUTO_INCREMENT  NOT NULL,
            `Department`    NVARCHAR (50)   NOT NULL,
            `Description`   NVARCHAR (50)   NOT NULL,
            `Price`         DECIMAL (4,2)   NOT NULL,
            PRIMARY KEY (`Id` ASC),
            UNIQUE (Department)
            );", con);

            MySqlCommand Create_specialist = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS specialist (
            `Id`            INT             AUTO_INCREMENT  NOT NULL,
            `Name`          NVARCHAR (50)   NOT NULL,
            `Department`    NVARCHAR (50)   NOT NULL,
            `Profession`    NVARCHAR (50)   NOT NULL,
            `Hospital`      NVARCHAR (50)   NOT NULL,
            `Expertise`     NVARCHAR (50)   NOT NULL,
            PRIMARY KEY (`Id` ASC),
            FOREIGN KEY (`Department`) REFERENCES specialistDepartment(`Department`)
            );", con);


            // generate root account

            MySqlCommand gen_root_acc = new MySqlCommand("REPLACE INTO `user` (`Id`, `PhotoPath`,`Username`,`Email`,`EmailVerified`,`TwoFactorVerified`,`Token`,`TokenExpiry`,`DateCreated`,`PasswordSalt`,`Password`,`ResetPwToken`,`ResetPwTokenExpiry`,`FailedAttempts`,`LastFailed`,`LastPwSet`,`Gender`,`DateOfBirth`,`Contact`,`Status`,`Role`,`Address`) VALUES ('999','Images/74b1e825-1fbe-4508-a7a7-b5ec86f8f366_icon.jpg','admin','admin@gmail.com',1,0,NULL,NULL,'2020-01-28','mbb8LPmet7Y=','cDt39OvddIC9uQf++ZeSsQDATAwtI4TlIswAWsgViCEBw/lAWgS16LDBvs47dCpbQOoq9Fu6mKKstCiYeYGedQ==',NULL,NULL,0,NULL,'2022-01-30 23:09:00','Male','2022-01-01 00:00:00','11111111','Active','Staff','ANG MO KIO AVENUE 8');", con);


            try
            {

                Create_table.ExecuteNonQuery();
                create_log_table.ExecuteNonQuery();
                create_surveyTable.ExecuteNonQuery();
                create_questionTable.ExecuteNonQuery();
                create_questionOptionTable.ExecuteNonQuery();

                Create_foodList.ExecuteNonQuery();
                Create_exerciseList.ExecuteNonQuery();
                Create_mealItems.ExecuteNonQuery();
                Create_exerciseRoutines.ExecuteNonQuery();
                Create_caloriesIntake.ExecuteNonQuery();

                create_surveyResponseTable.ExecuteNonQuery();
                Create_Blog.ExecuteNonQuery();
                Create_comments.ExecuteNonQuery();
                Create_specialistDepartment.ExecuteNonQuery();
                Create_specialist.ExecuteNonQuery();

                gen_root_acc.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }









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
            services.AddTransient<BlogService>();

            services.AddTransient<BookingService>();


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
                app.UseExceptionHandler("Error/Error");                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseStatusCodePages();

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
