using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.BusinessServices;
using PersonalStoreApplication.Models;
using System.Collections.Generic;

namespace PersonalStoreApplication.Controllers
{
    /// <summary>
    /// This class controller handles routing throughout the checkout process.
    /// </summary>
    public class CheckoutController : Controller
    {
        //Injected business services.
        ProductBusinessService pbs;
        RegistrationBusinessService rbs;

        public CheckoutController(ProductBusinessService pbs, RegistrationBusinessService rbs)
        {
            this.pbs = pbs;
            this.rbs = rbs;
        }

        /// <summary>
        /// This method gets all of the user's information and routes to the initial checkout page.
        /// </summary>
        /// <returns>A view of the initial checkout page.</returns>
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

        /// <summary>
        /// This method sets a product quantity in the user's cart.
        /// </summary>
        /// <param name="productId">The product id of the item to set the quantity for.</param>
        /// <param name="newAmount">The new quantity of the item.</param>
        /// <returns>A view of the updated checkout page.</returns>
        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult SetCartAmount(int productId, int newAmount)
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            //update the cart
            pbs.AddToCart(id, productId, newAmount, 0);

            return View("Checkout", pbs.GetUsersCart(id));
        }

        /// <summary>
        /// This method adds and address to the user's list of usable addresses.
        /// </summary>
        /// <param name="address">The address to add to the user's list.</param>
        /// <returns>A view of the updated checkout page with the newly added address.</returns>
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
