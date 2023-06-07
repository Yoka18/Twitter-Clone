using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
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
                    user.UserInfoId = Convert.ToInt32(rdr["Id"]);
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
        
        public PartialViewResult EditProfile(string id)
        {
            List<UserInfo> users = new List<UserInfo>();
            ViewBag.id = id;


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
                    user.UserInfoId = Convert.ToInt32(rdr["Id"]);
                    user.Name = rdr["Name"].ToString();
                    user.UserDesc = rdr["UserDesc"].ToString();
                    user.UserImage = rdr["UserImage"].ToString();
                    user.Location = rdr["Location"].ToString();
                    user.Name = rdr["Name"].ToString();
                    user.Username = rdr["Username"].ToString();
                    user.UserBackground = rdr["UserBackground"].ToString();
                    users.Add(user);
                }
                con.Close();
            }

            ViewBag.users = users;

            
            return PartialView();
        }
        
        
        [HttpPost]
        public ActionResult EditProfile(UserInfo user, HttpPostedFileBase file, HttpPostedFileBase background)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE UserInfo SET UserDesc=@UserDesc, UserImage=@UserImage, Name=@Name, Location=@Location WHERE Id=@Id";
                string sqlBackground = "UPDATE UserInfo SET UserBackground=@UserBackground WHERE Id=@Id";
                if (file != null && file.ContentLength > 0)
                {
                    string uploadPath = Server.MapPath("~/Uploads");
                    Guid guid = Guid.NewGuid();
                    string rawfileName = Path.GetFileName(file.FileName);
                    string fileName = guid.ToString() + "_" + rawfileName;
                    string filePath = Path.Combine(uploadPath, fileName);
                    file.SaveAs(filePath);
                    user.UserImage = fileName;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@UserDesc", user.UserDesc);
                        command.Parameters.AddWithValue("@UserImage", "Uploads/" + user.UserImage);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Location", user.Location);
                        command.Parameters.AddWithValue("@Id", user.UserInfoId);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    user.UserImage = "asd";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@UserDesc", user.UserDesc);
                        command.Parameters.AddWithValue("@UserImage", "Uploads/" + user.UserImage);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Location", user.Location);
                        command.Parameters.AddWithValue("@Id", user.UserInfoId);
                        command.ExecuteNonQuery();
                    }
                }


                if (background != null && background.ContentLength > 0)
                {
                    string uploadPath = Server.MapPath("~/Uploads");
                    Guid guid = Guid.NewGuid();
                    string rawfileName = Path.GetFileName(background.FileName);
                    string backgroundName = guid.ToString() + "_" + rawfileName;
                    string filePath = Path.Combine(uploadPath, backgroundName);
                    background.SaveAs(filePath);

                    user.UserBackground = backgroundName;

                    using (SqlCommand command = new SqlCommand(sqlBackground, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@UserBackground", "Uploads/" + user.UserBackground);
                        command.Parameters.AddWithValue("@Id", user.UserInfoId);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    user.UserBackground = "asd";
                    using (SqlCommand command = new SqlCommand(sqlBackground, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@UserBackground", "Uploads/" + user.UserBackground);
                        command.Parameters.AddWithValue("@Id", user.UserInfoId);
                        command.ExecuteNonQuery();
                    }
                }

                





                connection.Close();
            }



            return RedirectToAction("Index", "Profile", new { id = (string)Session["Username"] });


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