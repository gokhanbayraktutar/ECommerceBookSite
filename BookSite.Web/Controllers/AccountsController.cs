using BookSite.Data.Context;
using BookSite.Data.Entities;
using BookSite.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookSite.Web.Controllers
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }



    [RoutePrefix("")]
    public class AccountsController : Controller
    {
        DataContext db = new DataContext();
        HomeVM vm = new HomeVM();

        [Route("giris")]
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }
        [Route("giris")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Admin credentials)
        {
            //LOCAL AYARLARI 
            var response = Request["g-recaptcha-response"];
            const string secret = "6LcBa68cAAAAAJyI5EpcF2_ZdF9muCeBmXcXQcxF";

            //WebSite 

            //const string secret = "6Le7xDMdAAAAAAoeDFCd0H8ej_H6A85zq3491NeC";


            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            if (!captchaResponse.Success)
            {

                ViewBag.error = "Lütfen Kutucuğu işaretleyiniz!";
                return View();
            }



            var user = db.Users.FirstOrDefault(x => x.Email == credentials.Email && x.Password == credentials.Password);
            if (user != null)
            {
                string cookie = user.Email;
                FormsAuthentication.SetAuthCookie(cookie, true);
                Session["Email"] = user.Email;
                Session["ID"] = user.Id;
                return Redirect("/");
            }
            else
            {
                ViewBag.error = "Kullanıcı Adı Veya Şifre Hatalı!";
                return View();
            }
        }
        [Route("kaydol")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [Route("kaydol")]
        [HttpPost]
        public ActionResult Register(User user)
        {
            var user1 = db.Users.FirstOrDefault(x => x.Email == user.Email);

            if (user.Password == user.RePassword)
            {

                if (user == null)
                {

                    User user2 = new User();

                    user2.Name = user.Name;
                    user2.Lastname = user.Lastname;
                    user2.Email = user.Email;
                    user2.Phone = user.Phone;

                    ViewBag.error = "Bu Mail Adresi alınmış!";
                    return View(user2);

                }

                else
                {
                    

                    db.Users.Add(user);

                    db.SaveChanges();

                    TempData["kaydol"] = "1";

                    TempData.Keep();

                    return Redirect("/giris");
                }
            }

            else
            {
                User user2 = new User();

                user2.Name = user.Name;
                user2.Lastname = user.Lastname;
                user2.Email = user.Email;
                user2.Password = user.Password;
                user2.RePassword = user.RePassword;
                user2.Phone = user.Phone;

                ViewBag.error = "Şifreler eşleşmiyor!";
                return View(user2);
            }
        }

        [Route("cikis")]
        public ActionResult Logout()
        {
            HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();
            Session["Email"] = null;
            Session["ID"] = null;
            return Redirect("/");
        }

        [Route("hesabım")]
        public ActionResult MyAccount()
        {

            var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.User.Identity.Name);

            if(user != null)
            {

                vm.Carts = db.Carts.Where(x => x.UserID == user.Id && x.OrderStatus == "Order Completed").OrderByDescending(X=>X.OrderDate).ToList();

                vm.User = user;

                return View(vm);
            }

            else
            {
                return Redirect("/giris");
            }
           
        }

        [Route("hesabım")]
        [HttpPost]
        public ActionResult MyAccount( User User)
        {


            if (User.Password == User.RePassword)
            {
                db.Users.AddOrUpdate(User);
                db.SaveChanges();
                HttpContext.Session.Abandon();
                FormsAuthentication.SignOut();
                Session["Email"] = null;
                Session["ID"] = null;
                Session["FullName"] = null;
                User user = new User();

                TempData["uyari"] = "1";
                TempData.Keep();

                return View("Login", user);

            }

            else
            {
                vm.User = User;

                ViewBag.error = "Şifreler eşleşmiyor!";
                return View(vm);
            }
        }


    }
}