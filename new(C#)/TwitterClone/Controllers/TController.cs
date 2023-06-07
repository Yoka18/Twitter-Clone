using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class TController : Controller
    {
        public int publicID;
        string checkUser;

        public ActionResult P(int id, string test)
        {
            
            List<Tweet> tweets = new List<Tweet>();

            // web.config deki connectionStringden sunucuyu öğrenip localhosta bağlanıyor
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // sql bağlantısı başlıyor
                string sql = "SELECT * FROM Tweet WHERE Id = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                command.Parameters.AddWithValue("@Id", id);
                
                // gelen sql verileri reader yardımı ile okur
                SqlDataReader reader = command.ExecuteReader();
                // reader içindekileri burada değişkenlere aktarırız
                while (reader.Read())
                {
                    Tweet tweet = new Tweet();
                    tweet.TweetId = Convert.ToInt32(reader["Id"]);
                    tweet.Name = reader["Name"].ToString();
                    tweet.TweetDesc = reader["TweetDesc"].ToString();
                    tweet.Username = reader["Username"].ToString();
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

            ViewBag.Tweet = tweets;
            ViewBag.test = test;
            ViewBag.twtId = id;



            return View();
        }

        public ActionResult Edit(int id)
        {
            

            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Tweet WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        checkUser = reader["Username"].ToString();
                        
                    }
                    con.Close();
                }
            }



            if (Session["Username"] == null || Session["Username"].ToString() != checkUser)
            {
                return RedirectToAction("P", "T", new { id = id });
            }
            else
            {
                List<Tweet> tweets = new List<Tweet>();
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "SELECT * FROM Tweet WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        con.Open();
                        command.Parameters.AddWithValue("@Id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Tweet tweet = new Tweet();
                            tweet.TweetId = Convert.ToInt32(reader["Id"]);
                            tweet.Name = reader["Name"].ToString();
                            tweet.TweetDesc = reader["TweetDesc"].ToString();
                            tweet.Username = reader["Username"].ToString();
                            tweet.TweetLikes = Convert.ToInt32(reader["TweetLikes"]);
                            tweet.TweetComments = Convert.ToInt32(reader["TweetComments"]);
                            tweet.TweetRetweet = Convert.ToInt32(reader["TweetRetweet"]);
                            tweet.TweetShare = Convert.ToInt32(reader["TweetShare"]);
                            tweet.TweetImage = reader["TweetImage"].ToString();
                            tweets.Add(tweet);
                        }
                        con.Close();
                    }
                }

                ViewBag.tweets = tweets;
                ViewBag.twtId = id;


                return View();

            }

        }

        public PartialViewResult Comment(int id)
        {
            
            List<Comments> comments = new List<Comments>();
            List<Tweet> tweets = new List<Tweet>();
            List<string> blockedUsers = GetBlockedUsers();

            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Comments WHERE TweetId = @Id";
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Comments comment = new Comments();
                    comment.ComId = Convert.ToInt32(reader["ComId"]);
                    comments.Add(comment);
                }
                con.Close();
            }


            foreach (var comment in comments)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // sql bağlantısı başlıyor
                    string sql = "SELECT * FROM Tweet WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", comment.ComId);
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
            }
            

            ViewBag.tweets = tweets;


            return PartialView();
        }

        [HttpPost]
        public ActionResult PostComment(Tweet T, HttpPostedFileBase file, int tweetId)
        {
            int yeniID = 0;

            T.Name = (string)Session["Name"];
            T.Username = (string)Session["Username"];
            T.TweetImage = "asd";
            if (T.TweetDesc == null)
            {
                return RedirectToAction("asd");
            }

            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Tweet (Name, Username, TweetDesc, TweetImage, TweetComments, TweetLikes, TweetRetweet, TweetShare) VALUES (@Name, @Username, @TweetDesc, @TweetImage, @TweetComments, @TweetLikes, @TweetRetweet, @TweetShare); SELECT SCOPE_IDENTITY()";
                if (file != null && file.ContentLength > 0)
                {
                    string uploadPath = Server.MapPath("~/Uploads");
                    Guid guid = Guid.NewGuid();
                    string rawfileName = Path.GetFileName(file.FileName);
                    string fileName = guid.ToString() + "_" + rawfileName;
                    string filePath = Path.Combine(uploadPath, fileName);
                    file.SaveAs(filePath);
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

                        yeniID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
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

                        yeniID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                string commentsSql = "INSERT INTO Comments (ComId, TweetId) VALUES (@ComId, @TweetId)";
                using (SqlCommand commentsCommand = new SqlCommand(commentsSql, connection))
                {
                    commentsCommand.Parameters.Clear();
                    commentsCommand.Parameters.AddWithValue("@ComId", yeniID);
                    commentsCommand.Parameters.AddWithValue("@TweetId", tweetId);
                    commentsCommand.ExecuteNonQuery();
                }


                connection.Close();
            }

            return RedirectToAction("P", "T", new { id = tweetId });
        }
        [HttpPost]
        public ActionResult PostEdit(Tweet T, HttpPostedFileBase file, int tweetId)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Tweet SET TweetDesc=@TweetDesc, TweetImage=@TweetImage WHERE Id=@Id";
                if (file != null && file.ContentLength > 0)
                {
                    string uploadPath = Server.MapPath("~/Uploads");
                    Guid guid = Guid.NewGuid();
                    string rawfileName = Path.GetFileName(file.FileName);
                    string fileName = guid.ToString() + "_" + rawfileName;
                    string filePath = Path.Combine(uploadPath, fileName);
                    file.SaveAs(filePath);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@TweetDesc", T.TweetDesc);
                        command.Parameters.AddWithValue("@TweetImage", "Uploads/" + fileName);
                        command.Parameters.AddWithValue("@Id", tweetId);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@TweetDesc", T.TweetDesc);
                        command.Parameters.AddWithValue("@TweetImage", "asd");
                        command.Parameters.AddWithValue("@Id", tweetId);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
            return RedirectToAction("P", "T", new { id = tweetId });
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



    }
}