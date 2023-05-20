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
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Register()
        {
            return PartialView();
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
                            return RedirectToAction("Index", "Profile", new { id = L.Username });
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