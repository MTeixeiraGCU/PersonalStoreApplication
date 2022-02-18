using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.BusinessServices;

namespace PersonalStoreApplication.Controllers
{
    public class CheckoutController : Controller
    {
        ProductBusinessService pbs;

        public CheckoutController(ProductBusinessService pbs)
        {
            this.pbs = pbs;
        }

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult Index()
        {
            
            return View("Checkout", pbs.GetUsersCart((int)HttpContext.Session.GetInt32("userId")));
        }

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult Addresses()
        {
            return View("Addresses", null);
        }
    }
}
