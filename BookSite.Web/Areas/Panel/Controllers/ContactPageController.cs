
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
    public class ContactPageController : BaseController
    {
        DataContext db = new DataContext();

        [HttpGet]
        public ActionResult Edit()
        {

            var contactPage = db.ContactPages.FirstOrDefault(x => x.Id == 1);

            return View(contactPage);

        }
        [HttpPost]
        public JsonResult Edit(ContactPage data)
        {

                db.ContactPages.AddOrUpdate(data);

                db.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}