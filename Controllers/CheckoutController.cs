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
        RegistrationBusinessService rbs;

        public CheckoutController(ProductBusinessService pbs, RegistrationBusinessService rbs)
        {
            this.pbs = pbs;
            this.rbs = rbs;
        }

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult Index()
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            CheckoutModel model = new CheckoutModel();
            model.CartList = pbs.GetUsersCart(id);
            model.Addresses = rbs.GetAddresses(id);
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

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult AddAddress(Address address)
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            //add the address
            rbs.AddAddress(id, address);

            //reload the page
            CheckoutModel model = new CheckoutModel();
            model.CartList = pbs.GetUsersCart(id);
            model.Addresses = rbs.GetAddresses(id);
            model.Payment = PaymentType.Credit;

            return View("Checkout", model);
        }
    }
}
