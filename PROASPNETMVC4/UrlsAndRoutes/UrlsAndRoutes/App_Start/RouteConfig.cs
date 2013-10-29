using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {



            //23
            routes.RouteExistingFiles = true;
            routes.IgnoreRoute("Content/{filename}.html");
            routes.MapRoute("DiskFile", "Content/StaticContent.html",
            new {
            controller = "Customer",
            action = "List",
            });

            routes.MapRoute("ChromeRoute", "{*catchall}",
            new { controller = "Home", action = "Index" },
            new {
            customConstraint = new UserAgentConstraint("Chrome")
            },
            new[] { "UrlsAndRoutes.AdditionalControllers" });

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            new { controller = "Home", action = "Index",
            id = UrlParameter.Optional },
            new[] { "URLsAndRoutes.Controllers" });
            }


            //22
            //routes.RouteExistingFiles = true;

            //routes.MapRoute("DiskFile", "Content/StaticContent.html",
            //new {
            //controller = "Customer",
            //action = "List",
            //});

            //routes.MapRoute("ChromeRoute", "{*catchall}",
            //new { controller = "Home", action = "Index" },
            //new {
            //customConstraint = new UserAgentConstraint("Chrome")
            //},
            //new[] { "UrlsAndRoutes.AdditionalControllers" });

            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new { controller = "Home", action = "Index",
            //id = UrlParameter.Optional },
            //new[] { "URLsAndRoutes.Controllers" });
            //}



            //21
            //routes.RouteExistingFiles = true;
            //routes.MapRoute("ChromeRoute", "{*catchall}",
            //new { controller = "Home", action = "Index" },
            //new
            //{
            //    customConstraint = new UserAgentConstraint("Chrome")
            //},
            //new[] { "UrlsAndRoutes.AdditionalControllers" });
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //},
            //new[] { "URLsAndRoutes.Controllers" });


            //20
            //routes.MapRoute("ChromeRoute", "{*catchall}",
            //new { controller = "Home", action = "Index" },
            //new
            //{
            //    customConstraint = new UserAgentConstraint("Chrome")
            //},
            //new[] { "UrlsAndRoutes.AdditionalControllers" });

            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //},
            //new[] { "URLsAndRoutes.Controllers" });

            //19
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //new { controller = "^H.*", action = "Index|About",
            //httpMethod = new HttpMethodConstraint("GET") },
            //new[] { "URLsAndRoutes.Controllers" });
            

            //18
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //new { controller = "^H.*", action = "^Index$|^About$" },
            //new[] { "URLsAndRoutes.Controllers" });

            //17
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            // new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            // new { controller = "^H.*" },
            // new[] { "URLsAndRoutes.Controllers" });

            //16
            //Route myRoute = routes.MapRoute("AddContollerRoute",
            //"Home/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //},
            //new[] { "URLsAndRoutes.AdditionalControllers" });
            //myRoute.DataTokens["UseNamespaceFallback"] = false;


            //15
            //routes.MapRoute("AddContollerRoute", "Home/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //},
            //new[] { "URLsAndRoutes.AdditionalControllers" });
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //},
            //new[] { "URLsAndRoutes.Controllers" });

            //14
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //},
            //new[] { "URLsAndRoutes.AdditionalControllers", "UrlsAndRoutes.Controllers" });

            //13
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //},
            //new[] { "URLsAndRoutes.AdditionalControllers" });

            //12
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //});

            //11
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
            //new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //});

            //10
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",new
            //{
            //    controller = "Home",
            //    action = "Index",
            //    id = "DefaultId"
            //});

            //9
            //routes.MapRoute("ShopSchema2", "Shop/OldAction", new { controller = "Home", action = "Index" });
            //routes.MapRoute("ShopSchema", "Shop/{action}", new { controller = "Home" });
            //routes.MapRoute("", "X{controller}/{action}");
            //routes.MapRoute("MyRoute", "{controller}/{action}", new { controller = "Home", action = "Index" });
            //routes.MapRoute("", "Public/{controller}/{action}", new { controller = "Home", action = "Index" });

            //8
            //Let us imagine that we used to have a controller called Shop, which has now been replaced by the Home controller. 
            //Listing 13-13 shows how we can create a route to preserve the old URL schema.
            //routes.MapRoute("ShopSchema", "Shop/{action}",new { controller = "Home" });
            //routes.MapRoute("", "X{controller}/{action}");
            //routes.MapRoute("MyRoute", "{controller}/{action}",new { controller = "Home", action = "Index" });
            //routes.MapRoute("", "Public/{controller}/{action}",new { controller = "Home", action = "Index" });


            //7
            //routes.MapRoute("", "X{controller}/{action}");
            //routes.MapRoute("MyRoute", "{controller}/{action}",new { controller = "Home", action = "Index" });
            //routes.MapRoute("", "Public/{controller}/{action}",new { controller = "Home", action = "Index" });

            //6 Using Static URL Segments   http://mydomain.com/Public/Home/Index
            //routes.MapRoute("MyRoute", "{controller}/{action}",new { controller = "Home", action = "Index" });
            //routes.MapRoute("", "Public/{controller}/{action}",new { controller = "Home", action = "Index" });

            //5
            //routes.MapRoute("MyRoute", "{controller}/{action}",new { controller = "Home", action = "Index" });

            //4
            //routes.MapRoute("MyRoute", "{controller}/{action}",new { action = "Index" });

            //3   2 ile aynı şeyi yapıyor test metotlarında bu kullanılıyor.
            //routes.MapRoute("MyRoute", "{controller}/{action}");

            //2
            //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("MyRoute", myRoute);

            //1
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}