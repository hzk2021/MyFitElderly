using Microsoft.EntityFrameworkCore;
using EDP_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pract2.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDP_Project.Services
{
    public class UserService
    {
        private readonly UserDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public UserService(UserDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }



        //public List<User> GetAllUsers()
        //{
        //    List<User> AllUsers = new List<User>();
        //    AllUsers = _context.User.ToList();
        //    return AllUsers;

        //}

        public int retrieveuserid(string userID)
        {
            int uruserid = 0;
            string sql = "select * FROM User WHERE Username=@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Id"] != null)
                        {
                            if (reader["Id"] != DBNull.Value)
                            {
                                uruserid = (int)reader["Id"];
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return uruserid;

        }


        public string retrieveUserEmail(string userID)
        {
            string email = "";
            string sql = "select * FROM User WHERE Username=@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Email"] != null)
                        {
                            if (reader["Email"] != DBNull.Value)
                            {
                                email = reader["Email"].ToString();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return email;

        }



        public bool emailVerified(string userID)
        {
            bool verified = new bool();
            string sql = "select * FROM User WHERE Username=@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["EmailVerified"] != null)
                        {
                            if (reader["EmailVerified"] != DBNull.Value)
                            {
                                verified = (bool)reader["EmailVerified"];
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return verified;
        }


        public bool twofactorVerified(string userID)
        {
            bool verified = new bool();
            string sql = "select * FROM User WHERE Username=@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["TwoFactorVerified"] != null)
                        {
                            if (reader["TwoFactorVerified"] != DBNull.Value)
                            {
                                verified = (bool)reader["TwoFactorVerified"];
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return verified;
        }



        public bool exceededPwAge(string userId)
        {
            DateTime lastPwSet = new DateTime();
            con.Open();
            try
            {
                MySqlCommand cmdx = new MySqlCommand("SELECT * FROM USER WHERE USERNAME = @USERNAME", con);
                cmdx.Parameters.AddWithValue("@USERNAME", userId);
                cmdx.ExecuteScalar();

                using (MySqlDataReader reader = cmdx.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["LastPwSet"] != null)
                        {
                            if (reader["LastPwSet"] != DBNull.Value)
                            {
                                lastPwSet = (DateTime)reader["LastPwSet"];
                            }

                            else
                            {
                                return true;
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            // Minimum password age; if it has exceeded 3 days, then allow reset of password
            if (DateTime.Now.Subtract(lastPwSet).TotalDays > 30)
            {
                return true;
            }

            return false;

        }




        public string GetStaffUserName(string id_pk)
        {
            string username = "Invalid staffID";

            string sql = "select * FROM User WHERE Id=@ID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@ID", id_pk);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Username"] != null)
                        {
                            if (reader["Username"] != DBNull.Value)
                            {
                                username = (string)reader["Username"];
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            return username;

        }

        public bool isStaff(string userID)
        {
            var role = "";

            string sql = "select * FROM User WHERE Username=@USERID";
            MySqlCommand command = new MySqlCommand(sql, con);
            command.Parameters.AddWithValue("@USERID", userID);
            try
            {
                con.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Role"] != null)
                        {
                            if (reader["Role"] != DBNull.Value)
                            {
                                role = reader["Role"].ToString();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }


            if (role == "Staff")
            {
                return true;
            }


            return false;
        }

        public void AIOCheckStaff()
        {
            var HttpContext = _contextAccessor.HttpContext;

            if (HttpContext.Session.GetString("user") != null)
            {
                if (!isStaff(HttpContext.Session.GetString("user")))
                {
                    HttpContext.Response.Redirect("/");
                }


                if (!twofactorVerified(HttpContext.Session.GetString("user")))
                {
                    HttpContext.Response.Redirect("/Auth/TwoFactorAuth");
                } else
                {
                    if (!emailVerified(HttpContext.Session.GetString("user")))
                    {
                        HttpContext.Response.Redirect("/Auth/EmailVerification");
                    }

                    if (exceededPwAge(HttpContext.Session.GetString("user")))
                    {
                        HttpContext.Response.Redirect("/Auth/ResetPassword");
                    }
                }
            } else
            {
                HttpContext.Response.Redirect("/Login");
            }

        }

        public void AIOCheckGuest()
        {
            var HttpContext = _contextAccessor.HttpContext;

            if (HttpContext.Session.GetString("user") != null)
            {
                if (!twofactorVerified(HttpContext.Session.GetString("user")))
                {
                    HttpContext.Response.Redirect("/Auth/TwoFactorAuth");
                }
                else
                {
                    if (!emailVerified(HttpContext.Session.GetString("user")))
                    {
                        HttpContext.Response.Redirect("/Auth/EmailVerification");
                    }

                    if (exceededPwAge(HttpContext.Session.GetString("user")))
                    {
                        HttpContext.Response.Redirect("/Auth/ResetPassword");
                    }
                }
            }
            else
            {
                HttpContext.Response.Redirect("/Login");
            }

        }


    }







}
