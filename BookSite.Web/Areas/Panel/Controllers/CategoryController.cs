using BookSite.Data.Context;
using BookSite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class CategoryController : BaseController
    {
        DataContext db = new DataContext();

        // GET: Panel/Category
        public ActionResult Index()
        {
            var categoreies = db.Categories.ToList();

            return View(categoreies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(string name)
        {
           
                Category category = new Category();

                category.Name = name;

                db.Categories.Add(category);

                db.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = db.Categories.FirstOrDefault(x => x.Id == id);

            return View(category);

        }
        [HttpPost]
        public ActionResult Edit(int id, string name)
        {
            var category = db.Categories.FirstOrDefault(x => x.Id == id);

            if(category != null)
            {
                category.Name = name;

                db.Categories.AddOrUpdate(category);

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
            var category = db.Categories.FirstOrDefault(x => x.Id == id);

            if (category != null)
            {
                db.Categories.Remove(category);
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