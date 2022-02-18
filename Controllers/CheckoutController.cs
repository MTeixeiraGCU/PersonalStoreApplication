using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.BusinessServices;
using PersonalStoreApplication.Models;
using System.Collections.Generic;

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
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            CheckoutModel model = new CheckoutModel();
            model.CartList = pbs.GetUsersCart(id);
            model.Addresses = new List<Address>();
            model.Payment = PaymentType.Credit;

            return View("Checkout", model);
        }

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult SetCartAmount(int productId, int newAmount)
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            //update the cart
            pbs.AddToCart(id, productId, newAmount, 0);

            return View("Checkout", pbs.GetUsersCart(id));
        }
    }
}
