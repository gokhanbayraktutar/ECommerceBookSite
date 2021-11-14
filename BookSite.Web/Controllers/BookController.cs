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
    public class BookController : Controller
    {
        DataContext db = new DataContext();

        HomeVM vm = new HomeVM();


        [Route("kitapdetay/{id}")]
        public ActionResult BookDetail(int id)
        {
            vm.Book = db.Books.FirstOrDefault(x => x.Id == id);

            var catId = db.Book_Categories.FirstOrDefault(x => x.BookId == id).CategoryId;

            var BookIds = db.Book_Categories.Where(x => x.CategoryId == catId).Select(x => x.BookId).Take(5).ToList();


            foreach (var bookid in BookIds)
            {
                var cat_Book = db.Books.FirstOrDefault(x => x.Id == bookid);

                vm.CatBooks.Add(cat_Book);
            }


            return View(vm);
        }

        [HttpPost]

        [Route("kitapyorum")]
        public JsonResult AddComment(Book_Comment data)
        {

            data.Confirm = false;

            data.IP = System.Web.HttpContext.Current.Request.UserHostAddress;

            data.CommentHeader = null;

            data.Date = DateTime.Now;

            data.StarCount = data.StarCount;

            db.Book_Comments.Add(data);

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);

        }


        [Route("kitap-detayfavorilereekle")]
        public ActionResult AddFavourite(int BookId, int UserId)
        {

            Favorite favorite = new Favorite();

            favorite.BookId = BookId;

            favorite.UserId = UserId;

            db.Favorites.Add(favorite);

            db.SaveChanges();

            Book book = db.Books.FirstOrDefault(x => x.Id == BookId);


            return PartialView("_BookDetailFav", book);

        }

        [Route("kitap-detayfavorisil")]
        public ActionResult DeletetoFavourite(int BookId, int UserId)
        {

            Favorite favourite = db.Favorites.FirstOrDefault(x => x.BookId == BookId && x.UserId == UserId);

            db.Favorites.Remove(favourite);

            db.SaveChanges();

            Book book = db.Books.FirstOrDefault(x => x.Id == BookId);

            return PartialView("_BookDetailFav", book);

        }
        [Route("favorilerim")]
        public ActionResult MyFavourites()
        {

            TempData["styleFavourite"] = "grid";
            TempData.Keep();


            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var BookIds = db.Favorites.Where(x => x.UserId == user.Id).Select(x => x.BookId).ToList();

            List<Book> books = new List<Book>();

            foreach (var bookId in BookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookId);

                books.Add(book);
            }

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

            vm.Books = books.ToPagedList(1, 9);

            return View(vm);
        }
        [HttpPost]

       
        [Route("favorilersayfa/{pg}")]
        public ActionResult FavouritePage(int pg)
        {
            TempData["styleFavourite"] = "grid";
            TempData.Keep();


            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var BookIds = db.Favorites.Where(x => x.UserId == user.Id).Select(x => x.BookId).ToList();

            List<Book> books = new List<Book>();

            foreach (var bookId in BookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookId);

                books.Add(book);
            }

            vm.Books = books.ToPagedList(pg, 9);

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



            return View("MyFavourites", vm);


        }
        [Route("PageStyleFavourite")]
        public ActionResult PageStyleFavourite(string style)
        {
            TempData["styleFavourite"] = style;
            TempData.Keep();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        [Route("AddToCompare")]
        public ActionResult AddToCompare(int BookId)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            if(user != null)
            {
                List<CompareList> compareList = db.CompareLists.Where(x => x.UserId == user.Id).ToList();

                if (compareList.Count < 2)
                {
                    if (db.CompareLists.FirstOrDefault(x => x.BookId == BookId) == null)
                    {
                        CompareList compare = new CompareList();

                        compare.BookId = BookId;

                        compare.UserId = user.Id;

                        db.CompareLists.Add(compare);

                        db.SaveChanges();

                        return Json("Eklendi", JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        return Json("Bu kitap listede var!", JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    return Json("En fazla 2 kitap eklenebilir!", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Giriş yap", JsonRequestBehavior.AllowGet);
            }
        }


        [Route("karsilastirmaliste")]

        public ActionResult CompareList()
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var bookIds = db.CompareLists.Where(x => x.UserId == user.Id).Select(x => x.BookId).ToList();

            foreach (var bookid in bookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookid);

                vm.CompareBooks.Add(book);
            }

            return View(vm);
        }

        [Route("DeletetoCompare/{BookId}")]
        public ActionResult DeletetoCompare(int BookId)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var compare = db.CompareLists.FirstOrDefault(x => x.BookId == BookId && x.UserId == user.Id);


            db.CompareLists.Remove(compare);

            db.SaveChanges();

            var bookIds = db.CompareLists.Where(x => x.UserId == user.Id).Select(x => x.BookId).ToList();

            foreach (var bookid in bookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookid);

                vm.CompareBooks.Add(book);
            }


            return View("CompareList",vm);
        }





        [Route("coksatankitaplar")]
        public ActionResult BestSellerBooks()
        {
            TempData["styleBestSellerBooks"] = "grid";
            TempData.Keep();


            var Books = db.Books.Where(x => x.BestSellerStatus == true).ToList();

            vm.Books = Books.ToPagedList(1, 9);

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



            return View(vm);
        }

        [Route("coksatankitaplarsayfa/{pg}")]
        public ActionResult Page(int pg)
        {
            TempData["styleBestSellerBooks"] = TempData["styleBestSellerBooks"];
            TempData.Keep();


            var Books = db.Books.Where(x => x.BestSellerStatus == true).ToList();

            vm.Books = Books.ToPagedList(pg, 9);


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

            return View("BestSellerBooks", vm);


        }

        [Route("PageStyleBestSellerBooks")]
        public ActionResult PageStyle(string style)
        {
            TempData["styleBestSellerBooks"] = style;
            TempData.Keep();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [Route("yenicikankitaplar")]
        public ActionResult NewReleaseBooks()
        {
            TempData["styleNewReleaseBooks"] = "grid";
            TempData.Keep();


            var Books = db.Books.Where(x => x.NewReleasesStatus == true).ToList();

            vm.Books = Books.ToPagedList(1, 9);

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



            return View(vm);
        }

        [Route("yenicikankitaplarsayfa/{pg}")]
        public ActionResult NewReleaseBooksPage(int pg)
        {
            TempData["styleNewReleaseBooks"] = TempData["styleNewReleaseBooks"];
            TempData.Keep();


            var Books = db.Books.Where(x => x.NewReleasesStatus == true).ToList();

            vm.Books = Books.ToPagedList(pg, 9);


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

            return View("NewReleaseBooks", vm);


        }

        [Route("PageStyleNewReleaseBooks")]
        public ActionResult PageStyleNewReleaseBooks(string style)
        {
            TempData["styleNewReleaseBooks"] = style;
            TempData.Keep();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [Route("kampanyalikitaplar")]
        public ActionResult DealsBooks()
        {
            TempData["styleDealsBooks"] = "grid";
            TempData.Keep();


            var Books = db.Books.Where(x => x.DealsStatus == true).ToList();

            vm.Books = Books.ToPagedList(1, 9);

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



            return View(vm);
        }

        [Route("kampanyalikitaplarsayfa/{pg}")]
        public ActionResult DealsBooksPage(int pg)
        {
            TempData["styleDealsBooks"] = TempData["styleDealsBooks"];
            TempData.Keep();


            var Books = db.Books.Where(x => x.DealsStatus == true).ToList();

            vm.Books = Books.ToPagedList(pg, 9);


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

            return View("DealsBooks", vm);


        }

        [Route("PageStyleDealsBooks")]
        public ActionResult PageStyleDealsBooks(string style)
        {
            TempData["styleDealsBooks"] = style;
            TempData.Keep();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [Route("DeletetoFavouriteList")]
        public ActionResult DeletetoFavouriteList(int BookId, int UserId)
        {

            Favorite favourite = db.Favorites.FirstOrDefault(x => x.BookId == BookId && x.UserId == UserId);

            db.Favorites.Remove(favourite);

            db.SaveChanges();


            var favBookIds = db.Favorites.Where(x => x.User.Id == UserId).Select(x => x.BookId).ToList();

            List<Book> favbooks = new List<Book>();

            foreach (var Id in favBookIds)
            {
               var book = db.Books.FirstOrDefault(x => x.Id == Id);

                favbooks.Add(book);

            }


            TempData["style"] = TempData["style"];

            TempData.Keep();

            return PartialView("_BookFavouriteList", favbooks);

        }

    }
}