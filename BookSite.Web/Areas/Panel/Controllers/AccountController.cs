using BookSite.Data.Context;
using BookSite.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
    public class AccountController : Controller
    {
        DataContext db = new DataContext();

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Admin credentials)
        {
            //LOCAL AYARLARI 
            var response = Request["g-recaptcha-response"];
            //const string secret = "6LcBa68cAAAAAJyI5EpcF2_ZdF9muCeBmXcXQcxF";

            //WebSite 

            const string secret = "6Le7xDMdAAAAAAoeDFCd0H8ej_H6A85zq3491NeC";


            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //if (!captchaResponse.Success)
            //{

            //    ViewBag.error = "Lütfen Kutucuğu işaretleyiniz!";
            //    return View();
            //}


            var  admin = db.Admins.FirstOrDefault(x => x.UserName == credentials.UserName && x.Password == credentials.Password);
            if (admin != null)
            {
                string cookie = admin.UserName;
                FormsAuthentication.SetAuthCookie(cookie, true);
                Session["Name"] = admin.UserName;
                Session["ID"] = admin.Id;
                return Redirect("/Panel/Default/Index");
            }
            else
            {
                ViewBag.error = "Kullanıcı Adı Veya Şifre Hatalı!";
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();
            Session["FullName"] = null;
            Session["Picture"] = null;
            Session["ID"] = null;
            return Redirect("/Panel/Account/Login");
        }
    }
}
