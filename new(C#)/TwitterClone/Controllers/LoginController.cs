using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Register = TwitterClone.Models.Register;
using Login = TwitterClone.Models.Login;

namespace TwitterClone.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(string id)
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Register(Register R)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;

            string sqlCheckQuery = "SELECT COUNT(*) FROM login WHERE username = @username";
            string sqlInsertQuery = "INSERT INTO login (username, password) VALUES (@username, @password)";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Önce kullanıcı adının mevcut olup olmadığının kontrolü
                using (SqlCommand checkCommand = new SqlCommand(sqlCheckQuery, con))
                {
                    checkCommand.Parameters.AddWithValue("@username", R.Username);

                    int existingUserCount = (int)checkCommand.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        ViewBag.existedUser = "Kullanıcı mevcut";
                        return PartialView();
                    }
                    else
                    {
                        using (SqlCommand insertCommand = new SqlCommand(sqlInsertQuery, con))
                        {
                            insertCommand.Parameters.AddWithValue("@username", R.Username);
                            insertCommand.Parameters.AddWithValue("@password", R.Password);

                            int affectedRows = insertCommand.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                ViewBag.error = "Kullanıcı işlemi başarılı";
                                return PartialView();
                            }
                            else
                            {
                                ViewBag.error = "Bir hata oluştu sonra tekrar deneyin";
                                return PartialView();
                            }
                        }
                    }
                }

            }
        }

        [HttpGet]
        public PartialViewResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(Login L)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;


            string sqlQuery = "SELECT TOP 1 * FROM login WHERE username = @username AND password = @password;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", L.Username);
                    command.Parameters.AddWithValue("@password", L.Password);


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            FormsAuthentication.SetAuthCookie(L.Username, false);
                            Session["Username"] = L.Username;
                            // verinin yazıldığı şekilde girilmesini istiyorsam new{} şeklinde yazmalısın
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            // Kullanıcı bilgileri bulunamadı
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
        }

        
    }
}