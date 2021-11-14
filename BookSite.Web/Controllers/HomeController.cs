using BookSite.Data.Context;
using BookSite.Data.Entities;
using BookSite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        HomeVM vm = new HomeVM();

        public ActionResult Index()
        {

            vm.Sliders = db.Sliders.ToList();

            var catIds = db.Book_Categories.Select(x => x.CategoryId).Distinct().ToList();

            foreach (var catID in catIds)
            {
                var cat = db.Categories.FirstOrDefault(x => x.Id == catID);
                if (cat != null)
                {
                    vm.Categories.Add(cat);
                }
            }

            vm.BestSellerBooks = db.Books.Where(x => x.BestSellerStatus == true).Take(12).ToList();

            vm.NewReleasesBooks = db.Books.Where(x => x.NewReleasesStatus == true).Take(12).ToList();

            vm.DealsBooks = db.Books.Where(x => x.DealsStatus == true).Take(12).ToList();

            var chiLd_BookId = db.Book_Categories.Where(X => X.CategoryId == 4).Select(x => x.BookId).Distinct().ToList();

            foreach (var bookId in chiLd_BookId)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookId);

                if(book != null)
                {
                    vm.ChildrenBooks.Add(book);
                }
            }

                var Fav_BookIds=  db.Favorites.Select(x => x.BookId).Distinct().ToList();

            foreach (var bookId in Fav_BookIds)
            {
                var book = db.Books.FirstOrDefault(x => x.Id == bookId);

                if(book != null)
                {
                    vm.FavouriteBooks.Add(book);
                }
            }



            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("favorilereekle")]
        public ActionResult AddFavourite(int BookId, int UserId)
        {

            Favorite favorite = new Favorite();

            favorite.BookId = BookId;

            favorite.UserId = UserId;

            db.Favorites.Add(favorite);

            db.SaveChanges();

            Book book = db.Books.FirstOrDefault(x => x.Id == BookId);


            return PartialView("_Book", book);

        }

        [Route("DeletetoFavourite")]
        public ActionResult DeletetoFavourite(int BookId, int UserId)
        {

            Favorite favourite = db.Favorites.FirstOrDefault(x => x.BookId == BookId && x.UserId == UserId);

            db.Favorites.Remove(favourite);

            db.SaveChanges();

            Book book = db.Books.FirstOrDefault(x => x.Id == BookId);

            return PartialView("_Book", book);

        }
    }
}