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
    public class SliderController : Controller
    {
        DataContext db = new DataContext();

        // GET: Panel/Slider
        public ActionResult Index()
        {
            var sliders = db.Sliders.ToList();

            return View(sliders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public JsonResult CreateSlider()
        {
            Slider data = new Slider();

            var pic = System.Web.HttpContext.Current.Request.Files["ImageFile"];

            data.Header = Request.Form["Header"];

            data.Content = Request.Form["Content"];

            data.Sorting = Convert.ToInt32( Request.Form["Sorting"]);

            data.Active = Convert.ToBoolean(Request.Form["Active"]);

            string fname;

            string fileName = Path.GetFileName(pic.FileName);

            FileInfo fi = new FileInfo(fileName);

            if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg" || fi.Extension.ToLower() == ".png" || fi.Extension.ToLower() == ".gif" || fi.Extension.ToLower() == ".bmp")
            {
                fname = string.Format("{1}_{0}", fileName, DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace(":", "_"));

                string directory = "~/Upload/Image/" + fname;

                pic.SaveAs(Server.MapPath(directory));

                data.Picture = fname;

                db.Sliders.Add(data);

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
            var slider = db.Sliders.FirstOrDefault(x => x.Id == id);

            return View(slider);

        }
        [HttpPost]
        public JsonResult Edit()
        {

            Slider data = new Slider();

            data.Id = Convert.ToInt32(Request.Form["Id"]);

            var pic = System.Web.HttpContext.Current.Request.Files["ImageFile"];

            data.Header = Request.Form["Header"];

            data.Content = Request.Form["Content"];

            data.Sorting = Convert.ToInt32(Request.Form["Sorting"]);

            data.Active = Convert.ToBoolean(Request.Form["Active"]);

            string oldpicture = db.Sliders.FirstOrDefault(x => x.Id == data.Id).Picture;

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

                    db.Sliders.AddOrUpdate(data);

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

                data.Picture = oldpicture;

                db.Sliders.AddOrUpdate(data);

                db.SaveChanges();

                return Json(0, JsonRequestBehavior.AllowGet);
            }




        }
        public ActionResult Delete(int id)
        {
            var paymentType = db.Sliders.FirstOrDefault(x => x.Id == id);

            if (paymentType != null)
            {
                db.Sliders.Remove(paymentType);

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