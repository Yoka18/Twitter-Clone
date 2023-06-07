using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class SettingsController : Controller
    {
        [Authorize]
        public ActionResult Index(string id)
        {
            ViewBag.Id = id;



            string conString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                string sql = "SELECT * FROM UserInfo WHERE username = @username";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@username", id);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                        UserInfo user = new UserInfo();
                        user.UserInfoId = (int)rdr["Id"];
                        user.Name = rdr["Name"].ToString();
                        user.UserDesc = rdr["UserDesc"].ToString();
                        user.Email = rdr["Email"].ToString();
                        user.UserImage = rdr["UserImage"].ToString();
                        user.Following = Convert.ToInt32(rdr["Following"]);
                        user.Followers = Convert.ToInt32(rdr["Followers"]);
                        user.Joined = rdr["Joined"].ToString();
                        user.Location = rdr["Location"].ToString();
                        user.BirthDate = rdr["BirthDate"].ToString();
                        user.Username = rdr["Username"].ToString();
                        user.UserBackground = rdr["UserBackground"].ToString();
                        ViewBag.user = user;
                }

                con.Close();
            }

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql2 = "SELECT * FROM Login WHERE username = @username";

                SqlCommand cmd = new SqlCommand(sql2, con);
            
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@username", id);


                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Login login = new Login();
                    login.LoginId = (int)rdr["UserId"];
                    login.Email = rdr["Email"].ToString();
                    login.Username = rdr["Username"].ToString();
                    login.Password = rdr["Password"].ToString();
                    ViewBag.login = login;
                }
                con.Close();
            }


            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql2 = "SELECT * FROM Tweet WHERE username = @username";

                SqlCommand cmd = new SqlCommand(sql2, con);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@username", id);


                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Tweet tweet = new Tweet();
                    tweet.TweetId = (int)rdr["Id"];
                    ViewBag.tweet = tweet;
                }
                con.Close();
            }



            return View();
        }

        [HttpPost]
        public ActionResult EditUsername(int userinfoid, int loginid, string tweetid, UserInfo user)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string userinfo = "UPDATE UserInfo SET Username=@Username WHERE Id=@Id";
                string login = "UPDATE Login SET Username=@Username WHERE UserId=@Id";
                string tweet = "UPDATE Tweet SET Username = @Username WHERE Username = @Id";

                using (SqlCommand command = new SqlCommand(userinfo, connection))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Id", userinfoid);
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(login, connection))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Id", loginid);
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(tweet, connection))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Id", tweetid);
                    command.ExecuteNonQuery();
                }

                

                connection.Close();

            }

            Session.RemoveAll();
            return RedirectToAction("Index","Login");
        }

        [HttpPost]
        public ActionResult EditEmail(int userinfoid, int loginid, UserInfo user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string userinfo = "UPDATE UserInfo SET Email=@Email WHERE Id=@Id";
                string login = "UPDATE Login SET Email=@Email WHERE UserId=@Id";

                using (SqlCommand command = new SqlCommand(userinfo, connection))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Id", userinfoid);
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(login, connection))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Id", loginid);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
                return RedirectToAction("Index", "Profile", new {id = (string)Session["Username"] });
        }

        [HttpPost]
        public ActionResult EditPassword(int loginid, Login login)
        {
            string rentPass = null;
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string log = "SELECT Password FROM Login WHERE UserId = @Id";

                using (SqlCommand command = new SqlCommand(log, connection))
                {
                    command.Parameters.AddWithValue("@Id", loginid);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        rentPass = reader["Password"].ToString();
                    }

                    reader.Close();
                }

                connection.Close();
            }

            if (rentPass != null && login.CurrentPass == rentPass)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string log2 = "UPDATE Login SET Password = @Password WHERE UserId = @Id";

                    using (SqlCommand cmd = new SqlCommand(log2, con))
                    {
                        cmd.Parameters.AddWithValue("@Password", login.Password);
                        cmd.Parameters.AddWithValue("@Id", loginid);

                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }

            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }


    }
}