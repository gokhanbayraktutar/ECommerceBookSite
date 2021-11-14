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
    public class SearchController : Controller
    {
        DataContext db = new DataContext();

        HomeVM vm = new HomeVM();


        // GET: Search
        [Route("arama")]
        public ActionResult Index(string search)
        {
            TempData["styleSearch"] = "grid";

            TempData.Keep();

            search = search.ToLower();

            var SearchBooks = db.Books.Where(x => x.Name.ToLower() == search || x.Name.Contains(search)).ToList();
            //Kitaplar
            vm.Books = SearchBooks.OrderBy(x => x.Id).ToPagedList(1, 12);  //Kitaplar

            vm.BookCount = SearchBooks.Count();   //Kitap sayısı

            var Book_AuthorIds = db.Book_Authors.Select(x => x.AuthorId).Distinct().ToList();

            List<Author> authors = new List<Author>();

            foreach (var authorId in Book_AuthorIds)
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                authors.Add(author);
            }

            vm.Authors = authors.Where(x => x.Name.ToLower() == search || x.Name.Contains(search)).ToList();                // kitap olan yazarların search

            var Book_PublisherIds = db.Book_Publishers.Select(x => x.PublisherId).Distinct().ToList();

            List<Publisher> publishers = new List<Publisher>();

            foreach (var publisherId in Book_PublisherIds)
            {
                var publisher = db.Publishers.FirstOrDefault(x => x.Id == publisherId);

                publishers.Add(publisher);
            }

            foreach (var item in publishers)
            {
                string name = item.Name.ToLower();

                if (name.Contains(search))
                {
                    vm.Publishers.Add(item);
                }
            }
          /*  vm.Publishers = publishers.Where(x => x.Name.Contains(search)).ToList(); */       // kitap olan yayıncıların search

            TempData["search"] = search;

            TempData.Keep();

            ViewBag.Search = search;

            return View(vm);
        }
        [Route("aramasayfa/{pg}")]
        public ActionResult Page(int pg)
        {
            TempData["styleSearch"] = TempData["styleSearch"];
            TempData.Keep();


            string search = TempData["search"].ToString();

            search = search.ToLower();


            var SearchBooks = db.Books.Where(x => x.Name.ToLower() == search || x.Name.Contains(search)).ToList();

            vm.Books = SearchBooks.OrderBy(x => x.Id).ToPagedList(Convert.ToInt32(pg), 12);  //Kitaplar

            vm.BookCount = SearchBooks.Count();

            var Book_AuthorIds = db.Book_Authors.Select(x => x.AuthorId).Distinct().ToList();

            List<Author> authors = new List<Author>();

            foreach (var authorId in Book_AuthorIds)
            {
                var author = db.Authors.FirstOrDefault(x => x.Id == authorId);

                authors.Add(author);
            }

            vm.Authors = authors.Where(x => x.Name.ToLower() == search || x.Name.Contains(search)).ToList();

            var Book_PublisherIds = db.Book_Publishers.Select(x => x.PublisherId).Distinct().ToList();

            List<Publisher> publishers = new List<Publisher>();

            foreach (var publisherId in Book_PublisherIds)
            {
                var publisher = db.Publishers.FirstOrDefault(x => x.Id == publisherId);

                publishers.Add(publisher);
            }

            foreach (var item in publishers)
            {
                string name = item.Name.ToLower();

                if (name.Contains(search))
                {
                    vm.Publishers.Add(item);
                }
            }
            //vm.Publishers = publishers.Where(x => x.Name.ToLower() == search || x.Name.Contains(search)).ToList();


            TempData.Keep();

            ViewBag.Search = search;

            return View("Index",vm);
        }



        [Route("PageStyleSearch")]
        public ActionResult PageStyleSearch(string style)
        {
            TempData["styleSearch"] = style;
            TempData.Keep();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}