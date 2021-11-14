using BookSite.Data.Context;
using BookSite.Data.Entities;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class Book_AuthorController : BaseController
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var book_author = db.Book_Authors.ToList();

            return View(book_author);
        }

        [HttpGet]
        public ActionResult Create()
        {


            return View();

        }
        [HttpPost]
        public JsonResult Create(int bookid, int authorid)
        {
            Book_Author book_Author = new Book_Author();

            book_Author.BookId = bookid;

            book_Author.AuthorId = authorid;

            db.Book_Authors.Add(book_Author);

            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book_author = db.Book_Authors.FirstOrDefault(x => x.Id == id);

            return View(book_author);

        }
        [HttpPost]
        public JsonResult Edit(int selectAuthor, int book, int author)
        {

            Book_Author book_Author = db.Book_Authors.FirstOrDefault(x => x.BookId == book && x.AuthorId == author);

            book_Author.AuthorId = selectAuthor;

            db.Book_Authors.AddOrUpdate(book_Author);

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Delete(int id)
        {
            var book_author = db.Book_Authors.FirstOrDefault(x => x.Id == id);

            if (book_author != null)
            {
                db.Book_Authors.Remove(book_author);

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