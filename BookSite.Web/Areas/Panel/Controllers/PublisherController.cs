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
    public class PublisherController : BaseController
    {
        DataContext db = new DataContext();

        // GET: Panel/Publisher
        public ActionResult Index(int? pg)
        {

            if (pg == null)
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
                    IPagedList<Data.Entities.Publisher> list = db.Publishers.Where(x => x.Name.ToLower() == search || x.Name.ToLower().Contains(search)).OrderBy(x => x.Id).ToPagedList(1, 10);
                    return View(list);
                }
                else
                {
                    IPagedList<Data.Entities.Publisher> list = db.Publishers.OrderBy(x => x.Id).ToPagedList(1, 25);
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
                    IPagedList<Data.Entities.Publisher> list = db.Publishers.Where(x => x.Name.ToLower() == search || x.Name.ToLower().Contains(search)).OrderBy(x => x.Id).ToPagedList(Convert.ToInt32(pg), 10);
                    return View(list);
                }
                else
                {
                    IPagedList<Data.Entities.Publisher> list = db.Publishers.OrderBy(x => x.Id).ToPagedList(Convert.ToInt32(pg), 10);
                    return View(list);
                }
            }



        }

        public ActionResult Search(string search)
        {
            search = search.ToLower();

            TempData["search"] = search;

            TempData.Keep();

            IPagedList<Data.Entities.Publisher> list = db.Publishers.Where(x => x.Name.ToLower() == search || x.Name.ToLower().Contains(search)).OrderBy(x => x.Id).ToPagedList(1, 10);

            return View("Index", list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public JsonResult Create(Publisher publisher)
        {

            db.Publishers.Add(publisher);

            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var publisher = db.Publishers.FirstOrDefault(x => x.Id == id);

            return View(publisher);

        }
        [HttpPost]
        public JsonResult Edit(Publisher publisherdata)
        {
            var publisher = db.Publishers.FirstOrDefault(x => x.Id == publisherdata.Id);

            if (publisher != null)
            {

                db.Publishers.AddOrUpdate(publisherdata);

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
            var publisher = db.Publishers.FirstOrDefault(x => x.Id == id);

            if (publisher != null)
            {
                db.Publishers.Remove(publisher);
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