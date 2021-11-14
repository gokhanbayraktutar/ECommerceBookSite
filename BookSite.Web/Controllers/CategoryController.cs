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
    public class CategoryController : Controller
    {
        HomeVM vm = new HomeVM();

        DataContext db = new DataContext();

        [Route("kategoriler")]
        public ActionResult Index()
        {

            var Book_CategoryIds = db.Book_Categories.Select(x => x.CategoryId).Distinct().ToList();

            foreach (var catId in Book_CategoryIds)
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == catId);

                vm.Categories.Add(category);
            }

            return View(vm);
        }


        [Route("kategorikitap/{id}")]
        public ActionResult CategoryDetail(int id)
        {
            TempData["style"] = "grid";
            TempData.Keep();

            var BookIds = db.Book_Categories.Where(x => x.CategoryId == id).Select(x => x.BookId).Distinct().ToList();

            List<Book> books = new List<Book>();
           

            foreach (var bookid in BookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookid);

                 books.Add(book);
            }

            vm.Books = books.ToPagedList(1, 12);

            vm.BookCount = BookIds.Count();

            var Book_CategoryIds = db.Book_Categories.Select(x => x.CategoryId).Distinct().ToList();

            foreach (var catId in Book_CategoryIds)
            {
                var category = db.Categories.FirstOrDefault(x=>x.Id == catId);

                vm.Categories.Add(category);
            }

            var Book_AuthorIds = db.Book_Authors.Select(x => x.AuthorId).Distinct().ToList();

            foreach (var authorId in Book_AuthorIds)
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                vm.Authors.Add(author);
            }

            vm.Category = db.Categories.FirstOrDefault(x => x.Id == id);

            TempData["id"] = id;

            TempData.Keep();

            return View(vm);
        }

        [Route("kategorikitapsayfa/{pg}")]
        public ActionResult Page(int pg)
        {
            TempData["style"] = TempData["style"];
            TempData.Keep();

            int id = Convert.ToInt32(TempData["id"]);

            TempData.Keep();

            var BookIds = db.Book_Categories.Where(x => x.CategoryId == id).Select(x => x.BookId).Distinct().ToList();

            List<Book> books = new List<Book>();


            foreach (var bookid in BookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookid);

                books.Add(book);
            }

            vm.Books = books.ToPagedList(Convert.ToInt32(pg), 12);

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

            vm.Category = db.Categories.FirstOrDefault(x => x.Id == id);

            TempData.Keep();

            return View("CategoryDetail", vm);
        }

        [Route("PageStyle")]
        public ActionResult PageStyle(string style)
        {
            TempData["style"] = style;
            TempData.Keep();
            return Json("", JsonRequestBehavior.AllowGet);
        }


        [Route("AddtoFavouriteCat_Detail")]
        public ActionResult AddFavourite(int BookId, int UserId)
        {

            Favorite favorite = new Favorite();

            favorite.BookId = BookId;

            favorite.UserId = UserId;

            db.Favorites.Add(favorite);

            db.SaveChanges();

            Book book = db.Books.FirstOrDefault(x => x.Id == BookId);

            TempData["style"] = TempData["style"];

            TempData.Keep();

            return PartialView("_BookCatDetail", book);

        }


        [Route("DeletetoFavouriteCat_Detail")]
        public ActionResult DeletetoFavouriteCat_Detail(int BookId, int UserId)
        {

            Favorite favourite = db.Favorites.FirstOrDefault(x => x.BookId == BookId && x.UserId == UserId);

            db.Favorites.Remove(favourite);

            db.SaveChanges();

            Book book = db.Books.FirstOrDefault(x => x.Id == BookId);

            TempData["style"] = TempData["style"];

            TempData.Keep();

            return PartialView("_BookCatDetail", book);

        }
    }
}