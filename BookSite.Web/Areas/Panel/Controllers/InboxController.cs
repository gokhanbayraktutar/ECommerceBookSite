using BookSite.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class InboxController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var inboxes = db.Inboxes.ToList();

            return View(inboxes);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var inbox = db.Inboxes.FirstOrDefault(x => x.Id == id);

            return View(inbox);
        }



        [HttpPost]
        public JsonResult Edit(int id, string ReadStatus)
        {
            var inbox = db.Inboxes.FirstOrDefault(x => x.Id == id);

            inbox.Read = ReadStatus;

            db.Inboxes.AddOrUpdate(inbox);

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var inbox = db.Inboxes.FirstOrDefault(x => x.Id == id);

            if (inbox != null)
            {
                db.Inboxes.Remove(inbox);
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