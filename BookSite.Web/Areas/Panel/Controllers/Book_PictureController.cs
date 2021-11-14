using BookSite.Data.Context;
using BookSite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class Book_PictureController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var book_picture = db.Book_Pictures.ToList();

            return View(book_picture);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public JsonResult CreateBook_Picture()
        {
            //var pic = System.Web.HttpContext.Current.Request.Form["ImageUrl"];

            Image image = null;
            string randomFileName = "";
            string fullPath = "";
            try
            {
                string base64image = System.Web.HttpContext.Current.Request.Form["ImageUrl"];
                string[] stringSeparators = new string[] { "base64," };
                var t = base64image.Split(stringSeparators, StringSplitOptions.None)[1];  // remove data:image/jpeg;base64,
                byte[] bytes = Convert.FromBase64String(t);

                MemoryStream ms = new MemoryStream(bytes);


                image = Image.FromStream(ms);
                randomFileName = string.Format("{1}_{0}", "book.jpeg", DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace(":", "_"));
                fullPath = Server.MapPath("~/Upload/Image/" + randomFileName);
            }
            catch (Exception)
            {

            }


            Book_Picture data = new Book_Picture();

            

            data.BookId = Convert.ToInt32(Request.Form["BookId"]);

            //string fname;

            string fileName = Path.GetFileName(randomFileName);

            FileInfo fi = new FileInfo(fileName);

            if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg" || fi.Extension.ToLower() == ".png" || fi.Extension.ToLower() == ".gif")
            {
                image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                data.Picture = randomFileName;

                db.Book_Pictures.Add(data);

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
            var book_picture = db.Book_Pictures.FirstOrDefault(x => x.Id == id);

            return View(book_picture);

        }
        [HttpPost]
        public JsonResult Edit()
        {

            Image image = null;
            string randomFileName = "";
            string fullPath = "";
            try
            {
                string base64image = System.Web.HttpContext.Current.Request.Form["ImageUrl"];
                string[] stringSeparators = new string[] { "base64," };
                var t = base64image.Split(stringSeparators, StringSplitOptions.None)[1];  // remove data:image/jpeg;base64,
                byte[] bytes = Convert.FromBase64String(t);

                MemoryStream ms = new MemoryStream(bytes);


                image = Image.FromStream(ms);
                randomFileName = string.Format("{1}_{0}", "book.jpeg", DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace(":", "_"));
                fullPath = Server.MapPath("~/Upload/Image/" + randomFileName);
            }
            catch (Exception)
            {

            }


            Book_Picture data = new Book_Picture();

            data.Id = Convert.ToInt32(Request.Form["Id"]);

            //var pic = System.Web.HttpContext.Current.Request.Files["ImageFile"];

            data.BookId = Convert.ToInt32(Request.Form["BookId"]);

            string oldpicture = db.Book_Pictures.FirstOrDefault(x => x.Id == data.Id).Picture;

            //string fname;

            string fileName = Path.GetFileName(randomFileName);

            FileInfo fi = new FileInfo(fileName);

            if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg" || fi.Extension.ToLower() == ".png" || fi.Extension.ToLower() == ".gif")
            {


                image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                data.Picture = randomFileName;

                db.Book_Pictures.AddOrUpdate(data);

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

        public ActionResult Delete(int id)
        {
            var book_picture = db.Book_Pictures.FirstOrDefault(x => x.Id == id);

            string oldpicture = book_picture.Picture;

            if (book_picture != null)
            {
                db.Book_Pictures.Remove(book_picture);

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

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
    }
}