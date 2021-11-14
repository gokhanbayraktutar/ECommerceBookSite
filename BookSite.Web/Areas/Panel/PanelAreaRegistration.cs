using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel
{
    public class PanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Panel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
            name: "Panel",
            url: "Panel",
            defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
            namespaces: new[] { "BookSite.Web.Areas.Panel.Controllers" }
        );

            context.MapRoute(
                "Panel_default",
                "Panel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}