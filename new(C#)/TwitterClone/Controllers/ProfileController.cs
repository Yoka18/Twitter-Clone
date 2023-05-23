using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class ProfileController : Controller
    {


        //indexden sonra yazılan yazı id olarak dönüyor
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // test amaçlı kullanıcı ismi uyuşmasına göre tweetleri gelmesini istiyorum
            ViewBag.username = id;

            string conString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            
            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM UserInfo WHERE username = @username";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@username", id);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserInfo user = new UserInfo();
                    user.UserInfoId = Convert.ToInt32(rdr["Key"]);
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
                    ViewBag.user = user;
                }
                con.Close();
            }

            return View();
        }

        public PartialViewResult Ptwit(string user)
        {
            string users = user;

            List<Tweet> tweets = new List<Tweet>();
            // web.config deki connectionStringden sunucuyu öğrenip localhosta bağlanıyor
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // sql bağlantısı başlıyor
                string sql = "SELECT * FROM Tweet WHERE username = @username";
                SqlCommand command = new SqlCommand(sql, connection);
                // burada users veriimizi username'e aktararak sql komutunun istediğimiz şekilde çalışmasını sağlıyoruz
                command.Parameters.AddWithValue("@username", @users);
                connection.Open();

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

            ViewBag.Tweets = tweets;


            return PartialView();
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

            return RedirectToAction("Index", new {id = username});
        }

    }
}