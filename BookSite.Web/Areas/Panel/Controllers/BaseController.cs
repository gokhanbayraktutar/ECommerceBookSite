using BookSite.Web.AuthorizeAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    [PanelAuthorize]
    public class BaseController : Controller
    {
        // GET: Panel/Base
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}