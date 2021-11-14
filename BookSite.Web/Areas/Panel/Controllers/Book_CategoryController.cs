using BookSite.Data.Context;
using BookSite.Data.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class Book_CategoryController : BaseController
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var book_category = db.Book_Categories.ToList();

            return View(book_category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public JsonResult Create(int bookid, int categoryid)
        {
            Book_Category book_Category = new Book_Category();

            book_Category.BookId = bookid;

            book_Category.CategoryId = categoryid;

            db.Book_Categories.Add(book_Category);

            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book_category = db.Book_Categories.FirstOrDefault(x => x.Id == id);

            return View(book_category);

        }
        [HttpPost]
        public JsonResult Edit(int selectCategory, int book, int category)
        {

            Book_Category book_Category = db.Book_Categories.FirstOrDefault(x => x.BookId == book && x.CategoryId == category);

            book_Category.CategoryId = selectCategory;

            db.Book_Categories.AddOrUpdate(book_Category);

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Delete(int id)
        {
            var book_category = db.Book_Categories.FirstOrDefault(x => x.Id == id);

            if (book_category != null)
            {
                db.Book_Categories.Remove(book_category);
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