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
    public class CartController : BaseController
    {

        DataContext db = new DataContext();

        // GET: Panel/Book
        public ActionResult Index()
        {
            var carts = db.Carts.Where(x => x.OrderStatus != "In Cart" && x.OrderStatus != null).ToList();

            return View(carts);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var cart = db.Carts.FirstOrDefault(x => x.Id == id);

            return View(cart);

        }
        [HttpPost]
        public ActionResult Edit(Cart cartModel)
        {
            var dbcartModel = db.Carts.FirstOrDefault(x => x.Id == cartModel.Id);

            dbcartModel.OrderStatus = cartModel.OrderStatus;


            db.Carts.AddOrUpdate(dbcartModel);

            db.SaveChanges();

            var carts = db.Carts.Where(x => x.OrderStatus != "In Cart").OrderByDescending(x => x.OrderDate).ToList();

            return View("Index", carts);
        }


    }
}