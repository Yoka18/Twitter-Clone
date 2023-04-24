using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class ProfileController : Controller
    {
        
        // GET: Profile
        public ActionResult Index()
        {
            // test amaçlı kullanıcı ismi uyuşmasına göre tweetleri gelmesini istiyorum
            ViewBag.username = "inmisin";
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
                    tweet.TweetId = Convert.ToInt32(reader["Key"]);
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