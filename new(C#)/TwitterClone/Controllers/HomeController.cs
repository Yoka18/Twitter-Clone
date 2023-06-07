using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if ((string)Session["Username"] == null)
            {

            }
            else
            {
                string username = (string)Session["Username"];

                string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    string sql = "SELECT Name FROM UserInfo WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Session["Name"] = rdr["Name"];
                    }
                    con.Close();
                }
            }

            return View();
        }

        public PartialViewResult Twit()
        {
            List<Tweet> tweets = new List<Tweet>();
            List<string> blockedUsers = GetBlockedUsers(); // Engellenen kullanıcıların listesini al

            // web.config deki connectionStringden sunucuyu öğrenip localhosta bağlanıyor
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // sql bağlantısı başlıyor
                string sql = "SELECT * FROM Tweet ORDER BY Id DESC";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                // gelen sql verileri reader yardımı ile okur
                SqlDataReader reader = command.ExecuteReader();
                // reader içindekileri burada değişkenlere aktarırız
                while (reader.Read())
                {
                    string username = reader["Username"].ToString();

                    // Kullanıcı engellenmişse, tweet'i atlamak için kontrol yapılır
                    if (blockedUsers.Contains(username))
                        continue;

                    Tweet tweet = new Tweet();
                    tweet.TweetId = Convert.ToInt32(reader["Id"]);
                    tweet.Name = reader["Name"].ToString();
                    tweet.TweetDesc = reader["TweetDesc"].ToString();
                    tweet.Username = username;
                    tweet.TweetLikes = Convert.ToInt32(reader["TweetLikes"]);
                    tweet.TweetComments = Convert.ToInt32(reader["TweetComments"]);
                    tweet.TweetRetweet = Convert.ToInt32(reader["TweetRetweet"]);
                    tweet.TweetShare = Convert.ToInt32(reader["TweetShare"]);
                    tweet.TweetImage = reader["TweetImage"].ToString();
                    // tweets ismindeki List objesine tweet'ler ekleniyor
                    tweets.Add(tweet);
                }
                connection.Close();
            }

            ViewBag.Tweets = tweets;

            return PartialView();
        }

        private List<string> GetBlockedUsers()
        {
            
            List<string> blockedUsers = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

            
            if ((string)Session["Username"] == null)
            {

            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // sql sorgusu BanningUser username ile aynı olan bannedUser'ı getiriyor
                    string sql = "SELECT bannedUser FROM BannedFromOther WHERE banningUser = @username";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@username", Session["Username"].ToString());
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string bannedUser = reader["bannedUser"].ToString();
                        blockedUsers.Add(bannedUser);
                    }
                    connection.Close();
                }
            }

            return blockedUsers;
        }

        [HttpPost]
        public ActionResult PostTwit(Tweet T, HttpPostedFileBase file)
        {
            T.Name = (string)Session["Name"];
            T.Username = (string)Session["Username"];
            T.TweetImage = "asd";
            if (T.TweetDesc == null)
            {
                return RedirectToAction("asd");
            }

            if (file != null && file.ContentLength > 0)
            {
                // debug modda çalıştırma
                string uploadPath = Server.MapPath("~/Uploads");
                Guid guid = Guid.NewGuid();
                string rawfileName = Path.GetFileName(file.FileName);
                string fileName = guid.ToString() + "_" + rawfileName;
                string filePath = Path.Combine(uploadPath, fileName);
                file.SaveAs(filePath);


                string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Tweet (Name, Username, TweetDesc, TweetImage, TweetComments, TweetLikes, TweetRetweet, TweetShare) VALUES (@Name, @Username, @TweetDesc, @TweetImage, @TweetComments, @TweetLikes, @TweetRetweet, @TweetShare)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Name", T.Name);
                        command.Parameters.AddWithValue("@Username", T.Username);
                        command.Parameters.AddWithValue("@TweetDesc", T.TweetDesc);
                        command.Parameters.AddWithValue("@TweetImage", "Uploads/" + fileName);
                        command.Parameters.AddWithValue("@TweetComments", 0);
                        command.Parameters.AddWithValue("@TweetLikes", 0);
                        command.Parameters.AddWithValue("@TweetRetweet", 0);
                        command.Parameters.AddWithValue("@TweetShare", 0);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Tweet (Name, Username, TweetDesc, TweetImage, TweetComments, TweetLikes, TweetRetweet, TweetShare) VALUES (@Name, @Username, @TweetDesc, @TweetImage, @TweetComments, @TweetLikes, @TweetRetweet, @TweetShare)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Name", T.Name);
                        command.Parameters.AddWithValue("@Username", T.Username);
                        command.Parameters.AddWithValue("@TweetDesc", T.TweetDesc);
                        command.Parameters.AddWithValue("@TweetImage", "asd");
                        command.Parameters.AddWithValue("@TweetComments", 0);
                        command.Parameters.AddWithValue("@TweetLikes", 0);
                        command.Parameters.AddWithValue("@TweetRetweet", 0);
                        command.Parameters.AddWithValue("@TweetShare", 0);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Delete(int id, string username)
        {
            string conString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                // SQL sorgusunu hazırla
                string query = "DELETE FROM Tweet WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                // Bağlantıyı aç ve sorguyu çalıştır
                connection.Open();

                int resault = command.ExecuteNonQuery();

                // Reader'ı kullanmadan önce veritabanı bağlantısını kapat
                connection.Close();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult BlockUser(int id, string bannedUser)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            string sql = "INSERT INTO BannedFromOther (banningUser, BannedUser) VALUES (@banningUser, @BannedUser)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    com.Parameters.AddWithValue("@banningUser", Session["Username"].ToString());
                    com.Parameters.AddWithValue("@BannedUser", bannedUser);
                    com.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "Home");
        }


    }
}