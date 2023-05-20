using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Twit()
        {
            List<Tweet> tweets = new List<Tweet>();
            // web.config deki connectionStringden sunucuyu öğrenip localhosta bağlanıyor
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // sql bağlantısı başlıyor
                string sql = "SELECT * FROM Tweet";
                SqlCommand command = new SqlCommand(sql, connection);
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
            }

            ViewBag.Tweets = tweets;

            return PartialView();
        }
    }
}