using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: Panel/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}