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
    public class PaymentTypeController : Controller
    {
        DataContext db = new DataContext();

        // GET: Panel/PaymentTypes
        public ActionResult Index()
        {
            var paymentTypes = db.PaymentTypes.ToList();

            return View(paymentTypes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(string header, string content)
        {

            PaymentType paymentType = new PaymentType();

            paymentType.Header = header;

            paymentType.Content = content;

            db.PaymentTypes.Add(paymentType);

            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var paymentType = db.PaymentTypes.FirstOrDefault(x => x.Id == id);

            return View(paymentType);

        }
        [HttpPost]
        public ActionResult Edit(int id, string Header, string Content)
        {
            var paymentType = db.PaymentTypes.FirstOrDefault(x => x.Id == id);

            if (paymentType != null)
            {
                paymentType.Header = Header;

                paymentType.Content = Content;


                db.PaymentTypes.AddOrUpdate(paymentType);

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
            var paymentType = db.PaymentTypes.FirstOrDefault(x => x.Id == id);

            if (paymentType != null)
            {
                db.PaymentTypes.Remove(paymentType);

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
