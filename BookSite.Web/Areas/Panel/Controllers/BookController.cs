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
    public class BookController : BaseController
    {

        DataContext db = new DataContext();

        // GET: Panel/Book
        public ActionResult Index()
        {
            var books = db.Books.ToList();

            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public JsonResult Create(Book book)
        {
            var discount = book.Price * (book.DiscountPercentage*0.01);
            book.DiscountPrice = book.Price - discount;
            db.Books.Add(book);

            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == id);

            return View(book);

        }
        [HttpPost]
        public JsonResult Edit(Book bookdata)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == bookdata.Id);
            var discount = bookdata.Price * (bookdata.DiscountPercentage * 0.01);
            bookdata.DiscountPrice = bookdata.Price - discount;
            if (book != null)
            {
               
                db.Books.AddOrUpdate(bookdata);

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
            var book = db.Books.FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                db.Books.Remove(book);
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