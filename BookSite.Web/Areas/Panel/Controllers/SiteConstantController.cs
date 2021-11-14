
using BookSite.Data.Context;
using BookSite.Data.Entities;
using BookSite.Web.Models;
using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class SiteConstantController : Controller
    {
        DataContext db = new DataContext();

        

        [HttpGet]
        public ActionResult Edit(int id=1)
        {
            var siteConstant= db.SiteConstants.FirstOrDefault(x => x.Id == id);

            return View(siteConstant);

        }
        [HttpPost]
        public JsonResult Edit()
        {
            SiteConstant data = db.SiteConstants.FirstOrDefault();

            string oldLogo = data.Logo;

            var pic = System.Web.HttpContext.Current.Request.Files["ImageFile"];

            data.Content= Request.Form["Content"];

            data.Header = Request.Form["Header"];

            data.WorkTime = Request.Form["WorkTime"];

            string fname;
          

            if (pic != null)
            {
                string fileName = Path.GetFileName(pic.FileName);

                FileInfo fi = new FileInfo(fileName);

                if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg" || fi.Extension.ToLower() == ".png" || fi.Extension.ToLower() == ".gif")
                {
                    fname = string.Format("{1}_{0}", fileName, DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace(":", "_"));

                    string directory = "~/Upload/Image/" + fname;

                    pic.SaveAs(Server.MapPath(directory));

                    data.Logo = fname;

                    db.SiteConstants.AddOrUpdate(data);

                    db.SaveChanges();

                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(Server.MapPath($"~/Upload/Image/{oldLogo}"));

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
                db.SiteConstants.AddOrUpdate(data);
                db.SaveChanges();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}