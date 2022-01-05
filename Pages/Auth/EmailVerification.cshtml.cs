using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace EDP_Project.Pages
{
    public class EmailVerificationModel : PageModel
    {

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");


        public bool compareUserToken(string token)
        {

            DateTime expiry = new DateTime();


            // retriever form db

            // compare .

            //herer

            con.Open();

            var actToken = HttpUtility.UrlDecode(token).Replace(" ", "+");



            MySqlCommand cmdx = new MySqlCommand("SELECT TOKENEXPIRY FROM USER WHERE TOKEN = @TOKEN", con);
            cmdx.Parameters.AddWithValue("@TOKEN", actToken);
            cmdx.ExecuteScalar();

            try
            {
                using (MySqlDataReader reader = cmdx.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["TokenExpiry"] != null)
                        {
                            if (reader["TokenExpiry"] != DBNull.Value)
                            {
                                expiry = (DateTime)reader["TokenExpiry"];
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }






            if (expiry.Subtract(DateTime.Now).TotalMinutes < 30)


            {

                //string h = null;
                //string sql = "select Token FROM User WHERE TOKEN = @USERTOKEN";

                try
                {




                    MySqlCommand cmd = new MySqlCommand("select * FROM User WHERE TOKEN = @USERTOKEN", con);
                    cmd.Parameters.AddWithValue("@USERTOKEN", actToken);
                    int tokenExists = Convert.ToInt32(cmd.ExecuteScalar());




                    if (tokenExists > 0)
                    {

                        // if exists, determiner whether link has expired.





                        // Update email verificaiton


                        try
                        {
                            string sql = "UPDATE User SET EMAILVERIFIED = @STATUS, TOKEN = @TOKEN, TOKENEXPIRY = @TOKENEXPIRY WHERE TOKEN=@USERTOKEN; ";
                            using (var cmd2 = new MySqlCommand(sql, con))
                            {
                                cmd2.Parameters.AddWithValue("@USERTOKEN", actToken);
                                cmd2.Parameters.AddWithValue("@STATUS", 1);
                                cmd2.Parameters.AddWithValue("@TOKEN", DBNull.Value);
                                cmd2.Parameters.AddWithValue("@TOKENEXPIRY", DBNull.Value);
                                var update = cmd2.ExecuteNonQuery();
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

                        return true;


                    }

                }

                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }




            }


            return false;

        }


        public IActionResult OnGet()
        {

            if (compareUserToken(HttpContext.Request.Query["token"]))
            {
                return Redirect("~/");
            }
            else return RedirectToPage("VerificationFailed");





        }


        public IActionResult OnPost()
        {

            // TODO: Send email here

            return Page();

        }


    }



}
