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
    public class AuthorController : BaseController
    {
        DataContext db = new DataContext();

        // GET: Panel/Author
        public ActionResult Index(int? pg)
        {

            if(pg == null)
            {
                TempData["search"] = TempData["search"];

                TempData.Keep();

                string search = null;
                try
                {
                    search = TempData["search"].ToString();
                }
                catch (Exception)
                {

                }
                if (!string.IsNullOrEmpty(search))
                {
                    IPagedList<Data.Entities.Author> list = db.Authors.Where(x => x.Name.ToLower() == search || x.Name.ToLower().Contains(search)).OrderBy(x => x.Id).ToPagedList(1, 10);
                    return View(list);
                }
                else
                {
                    IPagedList<Data.Entities.Author> list = db.Authors.OrderBy(x => x.Id).ToPagedList(1, 25);
                    return View(list);
                }



              
            }

            else
            {

                TempData["search"] = TempData["search"];
                TempData.Keep();
                string search = null;
                try
                {
                    search = TempData["search"].ToString();
                }
                catch (Exception)
                {

                }
                if (!string.IsNullOrEmpty(search))
                {
                    IPagedList<Data.Entities.Author> list = db.Authors.Where(x => x.Name.ToLower() == search || x.Name.ToLower().Contains(search)).OrderBy(x => x.Id).ToPagedList(Convert.ToInt32(pg), 10);
                    return View(list);
                }
                else
                {
                    IPagedList<Data.Entities.Author> list = db.Authors.OrderBy(x => x.Id).ToPagedList(Convert.ToInt32(pg), 10);
                    return View(list);
                }
            }

           

        }

        public ActionResult Search(string search)
        {
            search = search.ToLower();

            TempData["search"] = search;

            TempData.Keep();

            IPagedList<Data.Entities.Author> list = db.Authors.Where(x => x.Name.ToLower() == search || x.Name.ToLower().Contains(search)).OrderBy(x => x.Id).ToPagedList(1, 10);

            return View("Index", list);
        } 




        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public JsonResult Create(Author author)
        {

            db.Authors.Add(author);

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var author = db.Authors.FirstOrDefault(x => x.Id == id);

            return View(author);

        }
        [HttpPost]
        public JsonResult Edit(Author authordata)
        {
            var author = db.Authors.FirstOrDefault(x => x.Id == authordata.Id);

            if (author != null)
            {

                db.Authors.AddOrUpdate(authordata);

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
            var author = db.Authors.FirstOrDefault(x => x.Id == id);

            if (author != null)
            {
                db.Authors.Remove(author);
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