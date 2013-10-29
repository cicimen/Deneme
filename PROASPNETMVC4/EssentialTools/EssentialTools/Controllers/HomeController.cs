using EssentialTools.Models;
using System.Web.Mvc;
using System.Linq;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private Product[] products = {
        new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
        new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
        new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
        new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        private IValueCalculator calc;
        public HomeController(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public ActionResult Index()
        {
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            //Our goal with Ninject is to reach the point where we specify that we want to instantiate an
            //implementation of the IValueCalculator interface, but the details of which implementation is required
            //are not part of the code in the Home controller.
            //IValueCalculator calc = new LinqValueCalculator();
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}