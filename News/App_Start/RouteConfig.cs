using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace News
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("NewsDetail", "haberler/{categoryname}/{newsname}/{newsid}", new { controller = "Home", action = "NewsDetail" });

            routes.MapRoute("NewsTagList", "haberler/{tagname}/{tagid}", new { controller= "Home", action = "NewsTagList"});

            routes.MapRoute("NewsCategoryList", "haberler/{categoryname}", new { controller = "Home", action = "NewsCategoryList" });

            //En alt kısımda olacak (Routing doğru bir biçimde çalışması için)
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
