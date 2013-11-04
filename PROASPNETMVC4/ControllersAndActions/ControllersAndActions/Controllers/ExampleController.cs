using System;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        //public ViewResult Index()
        //{
        //    return View("Homepage");
        //}

        public ViewResult Index()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View((object)"Hello, World");
        }

        //public RedirectResult Redirect()
        //{
        //    return Redirect("/Example/Index");
        //}

        //public RedirectResult Redirect()
        //{
        //    return RedirectPermanent("/Example/Index");
        //}

        public RedirectToRouteResult Redirect()
        {
            return RedirectToRoute(new
            {
                controller = "Example",
                action = "Index",
                ID = "MyID"
            });
        }

        //public HttpStatusCodeResult StatusCode()
        //{
        //    return new HttpStatusCodeResult(404, "URL cannot be serviced");
        //}

        public HttpStatusCodeResult StatusCode()
        {
            return new HttpUnauthorizedResult();
        }

    }
}