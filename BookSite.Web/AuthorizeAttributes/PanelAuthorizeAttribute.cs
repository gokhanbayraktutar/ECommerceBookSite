using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookSite.Web.AuthorizeAttributes
{
    public class PanelAuthorizeAttribute : AuthorizeAttribute
    {
        public Data.Context.DataContext db { get; set; }
        public Data.Entities.Admin AktifKullanici { get; set; }

        public PanelAuthorizeAttribute()
        {
            db = new Data.Context.DataContext();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
          
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                AktifKullanici = db.Admins.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);

                if (AktifKullanici==null)
                {
                    filterContext.Result = new RedirectToRouteResult(new
              RouteValueDictionary(new { controller = "Account", action = "Logout", area = "Panel" }));
                }
                else
                {
                   
                }

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login", area = "Panel" }));
            }
        }
    }
}