using BookSite.Data.Context;
using BookSite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class CargoController : Controller
    {

        DataContext db = new DataContext();

        // GET: Panel/Book
        public ActionResult Index()
        {
            var cargos = db.Cargos.ToList();

            return View(cargos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public JsonResult CreateCargo()
        {
            Cargo data = new Cargo();

            var pic = System.Web.HttpContext.Current.Request.Files["ImageFile"];

            data.CompanyName = Request.Form["CompanyName"];

            data.Price = Convert.ToDouble(Request.Form["Price"]);

            string fname;

            string fileName = Path.GetFileName(pic.FileName);

            FileInfo fi = new FileInfo(fileName);

            if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg" || fi.Extension.ToLower() == ".png" || fi.Extension.ToLower() == ".gif" || fi.Extension.ToLower() == ".bmp")
            {
                fname = string.Format("{1}_{0}", fileName, DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace(":", "_"));

                string directory = "~/Upload/Image/" + fname;

                pic.SaveAs(Server.MapPath(directory));

                data.Picture = fname;

                db.Cargos.Add(data);

                db.SaveChanges();

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book_picture = db.Cargos.FirstOrDefault(x => x.Id == id);

            return View(book_picture);

        }
        [HttpPost]
        public JsonResult Edit()
        {

            Cargo data = new Cargo();

            data.Id = Convert.ToInt32(Request.Form["Id"]);

            data.CompanyName = Request.Form["CompanyName"];

            data.Price = Convert.ToDouble(Request.Form["Price"]);

            var pic = System.Web.HttpContext.Current.Request.Files["ImageFile"];

            string oldpicture = db.Cargos.FirstOrDefault(x => x.Id == data.Id).Picture;

            string fname;


            if (pic != null)
            {
                string fileName = Path.GetFileName(pic.FileName);

                FileInfo fi = new FileInfo(fileName);

                if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg" || fi.Extension.ToLower() == ".png" || fi.Extension.ToLower() == ".gif" || fi.Extension.ToLower() == ".bmp")
                {
                    fname = string.Format("{1}_{0}", fileName, DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace(":", "_"));

                    string directory = "~/Upload/Image/" + fname;

                    pic.SaveAs(Server.MapPath(directory));

                    data.Picture = fname;

                    db.Cargos.AddOrUpdate(data);

                    db.SaveChanges();

                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(Server.MapPath($"~/Upload/Image/{oldpicture}"));

                    if (fileInfo.Exists)
                    {
                        try
                        {
                            fileInfo.Delete();
                        }
                        catch (Exception)
                        {

                        }
                    }
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                db.Cargos.AddOrUpdate(data);

                db.SaveChanges();

                return Json(0, JsonRequestBehavior.AllowGet);
            }




        }
        public ActionResult Delete(int id)
        {
            var paymentType = db.Cargos.FirstOrDefault(x => x.Id == id);

            if (paymentType != null)
            {
                db.Cargos.Remove(paymentType);

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