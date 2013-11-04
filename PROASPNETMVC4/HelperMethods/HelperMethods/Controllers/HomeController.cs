using System.Web.Mvc;
using HelperMethods.Models;

namespace HelperMethods.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Fruits = new string[] { "Apple", "Orange", "Pear" };
            ViewBag.Cities = new string[] { "New York", "London", "Paris" };
            string message = "This is an HTML element: <input>";
            return View((object)message);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person person)
        {
            return View(person);
        }

        public ActionResult CreatePerson2()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson2(Person person)
        {
            return View(person);
        }


        public ActionResult CreatePerson3()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson3(Person person)
        {
            return View("DisplayPerson", person);
        }

        public ActionResult CreatePerson4()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson4(Person person)
        {
            return View("DisplayPerson", person);
        }

    }
}