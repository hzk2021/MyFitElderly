using Microsoft.EntityFrameworkCore;
using EDP_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pract2.Models;
using MySql.Data.MySqlClient;

namespace EDP_Project.Services
{
    public class UserService
    {
        private readonly UserDbContext _context;

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public UserService(UserDbContext context)
        {
            _context = context;
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
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return uruserid;

        }

        public string GetStaffUserName(string id_pk)
        {
            string username = string.Empty;

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

    }



    



}
