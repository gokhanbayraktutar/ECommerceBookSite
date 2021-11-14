using BookSite.Data.Context;
using BookSite.Data.Entities;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class Book_PublisherController : BaseController
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var book_publisher = db.Book_Publishers.ToList();

            return View(book_publisher);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(int bookid, int publisherid)
        {
            Book_Publisher book_Publisher = new Book_Publisher();

            book_Publisher.BookId = bookid;

            book_Publisher.PublisherId = publisherid;

            db.Book_Publishers.Add(book_Publisher);

            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book_publisher = db.Book_Publishers.FirstOrDefault(x => x.Id == id);

            return View(book_publisher);

        }
        [HttpPost]
        public JsonResult Edit(int selectPublisher, int book, int publisher)
        {

            Book_Publisher book_Publisher = db.Book_Publishers.FirstOrDefault(x => x.BookId == book && x.PublisherId == publisher);

            book_Publisher.PublisherId = selectPublisher;

            db.Book_Publishers.AddOrUpdate(book_Publisher);

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Delete(int id)
        {
            var book_publisher = db.Book_Publishers.FirstOrDefault(x => x.Id == id);

            if (book_publisher != null)
            {
                db.Book_Publishers.Remove(book_publisher);

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