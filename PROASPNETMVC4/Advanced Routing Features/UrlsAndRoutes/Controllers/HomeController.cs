using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        //public ActionResult CustomVariable(string id)
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    //ViewBag.CustomVariable = RouteData.Values["id"];

        //    //ViewBag.CustomVariable = id;
        //    ViewBag.CustomVariable = id == null ? "<no value>" : id;
        //    return View();
        //}

        //public ActionResult CustomVariable(string id = "DefaultId")
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    ViewBag.CustomVariable = id;
        //    return View();
        //}

        public ActionResult CustomVariable(string id = "DefaultId")
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = id;
            return View();
        }

        public ViewResult MyActionMethod()
        {
            string myActionUrl = Url.Action("Index", new { id = "MyID" });
            string myRouteUrl = Url.RouteUrl(new { controller = "Home", action = "Index" });
            //... do something with URLs...
            return View();
        }

    }
}
