using BookSite.Data.Context;
using BookSite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class AdminController : BaseController
    {
        DataContext db = new DataContext();

        // GET: Panel/Admin
        public ActionResult Index()
        {
            var admins = db.Admins.ToList();

            return View(admins);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(string username ,string password,string email)
        {
            
                Admin admin = new Admin();

                admin.UserName = username;

                admin.Email = email;

                admin.Password = password;

                db.Admins.Add(admin);

                db.SaveChanges();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var admin = db.Admins.FirstOrDefault(x => x.Id == id);

            return View(admin);

        }
        [HttpPost]
        public ActionResult Edit(int id, string username , string password, string email)
        {
            var admin = db.Admins.FirstOrDefault(x => x.Id == id);

            if(admin != null)
            {
                admin.UserName = username ;

                admin.Email = email ;

                admin.Password = password ;

                db.Admins.AddOrUpdate(admin);

                db.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult Delete(int id)
        {
            var admin = db.Admins.FirstOrDefault(x => x.Id == id);

            if (admin != null)
            {
                db.Admins.Remove(admin);
                db.SaveChanges();
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}