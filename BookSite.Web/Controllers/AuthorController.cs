using BookSite.Data.Context;
using BookSite.Data.Entities;
using BookSite.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Controllers
{
    [RoutePrefix("")]
    public class AuthorController : Controller
    {
        DataContext db = new DataContext();

        HomeVM vm = new HomeVM();

        [Route("yazarlar")]
        // GET: Author
        public ActionResult Index()
        {
            var Book_AuthorIds = db.Book_Authors.Select(x => x.AuthorId).Distinct().ToList();

            List<Author> authors = new List<Author>();

            foreach (var authorId in Book_AuthorIds)
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                authors.Add(author);
            }
            vm.PagedListAuthors = authors.ToPagedList(1, 15);

            return View(vm);
        }

        [Route("yazarlarsayfa/{pg}")]
        public ActionResult Page(int pg)
        {
            var Book_AuthorIds = db.Book_Authors.Select(x => x.AuthorId).Distinct().ToList();

            List<Author> authors = new List<Author>();

            foreach (var authorId in Book_AuthorIds)
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                authors.Add(author);
            }
            vm.PagedListAuthors = authors.ToPagedList(Convert.ToInt32(pg), 15);

            return View("Index", vm);
        }


        [Route("yazarkitap/{id}")]
        public ActionResult AuthorDetail(int id)
        {
            TempData["styleAuthor"] = "grid";
            TempData.Keep();

            var BookIds = db.Book_Authors.Where(x => x.AuthorId == id).Select(x => x.BookId).Distinct().ToList();

            List<Book> books = new List<Book>();


            foreach (var bookid in BookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookid);

                books.Add(book);
            }

            vm.Books = books.ToPagedList(1, 2);

            vm.BookCount = BookIds.Count();

            var Book_CategoryIds = db.Book_Categories.Select(x => x.CategoryId).Distinct().ToList();

            foreach (var catId in Book_CategoryIds)
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == catId);

                vm.Categories.Add(category);
            }

            var Book_AuthorIds = db.Book_Authors.Select(x => x.AuthorId).Distinct().ToList();

            foreach (var authorId in Book_AuthorIds)
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                vm.Authors.Add(author);
            }

            vm.Author = db.Authors.FirstOrDefault(x => x.Id == id);

            TempData["id"] = id;

            TempData.Keep();

            return View(vm);
        }

        [Route("yazarkitapsayfa/{pg}")]
        public ActionResult PageAuthor(int pg)
        {
            TempData["styleAuthor"] = TempData["styleAuthor"];
            TempData.Keep();

            int id = Convert.ToInt32(TempData["id"]);

            TempData.Keep();

            var BookIds = db.Book_Authors.Where(x => x.AuthorId == id).Select(x => x.BookId).Distinct().ToList();

            List<Book> books = new List<Book>();


            foreach (var bookid in BookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookid);

                books.Add(book);
            }

            vm.Books = books.ToPagedList(Convert.ToInt32(pg), 2);

            vm.BookCount = BookIds.Count();

            var Book_CategoryIds = db.Book_Categories.Select(x => x.CategoryId).Distinct().ToList();

            foreach (var catId in Book_CategoryIds)
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == catId);

                vm.Categories.Add(category);
            }

            var Book_AuthorIds = db.Book_Authors.Select(x => x.AuthorId).Distinct().ToList();

            foreach (var authorId in Book_AuthorIds)
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                vm.Authors.Add(author);
            }

            vm.Author = db.Authors.FirstOrDefault(x => x.Id == id);

            TempData.Keep();

            return View("AuthorDetail", vm);
        }

        [Route("PageStyleAuthor")]
        public ActionResult PageStyle(string style)
        {
            TempData["styleAuthor"] = style;
            TempData.Keep();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}