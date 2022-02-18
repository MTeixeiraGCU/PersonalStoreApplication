using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.BusinessServices;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    /// <summary>
    /// This controller handles all the product actions including searches and adding/editing products by an administrator.
    /// </summary>
    public class ProductController : Controller
    {
        //handles logic and database queries for products.
        ProductBusinessService pbs;

        //Default constructor, requires ProductBusinessService Injection
        public ProductController(ProductBusinessService pbs)
        {
            this.pbs = pbs;
        }

        /// <summary>
        /// This is the entry point for the product controller. This will always redirect to the home page.
        /// </summary>
        /// <returns>A view containing the home page.</returns>
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This action routes to the search form view.
        /// </summary>
        /// <returns>A view for the search form.</returns>
        public IActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// This action process the incoming token from the search form.
        /// </summary>
        /// <param name="token">search pattern to match against the products with.</param>
        /// <returns>A view list containing all the matching products.</returns>
        public IActionResult ProcessSearch(string token)
        {
            ViewBag.Message = token;
            return View("SearchResults", pbs.SearchForProducts(token));
        }

        /// <summary>
        /// This routing methods handles access of a users personal cart.
        /// </summary>
        /// <returns>A view containing the list of all items in the users cart.</returns>
        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult Cart()
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            return View("Cart", pbs.GetUsersCart(id));
        }

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult AddProductToCart(int productId, int current)
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            //add product to their cart
            pbs.AddToCart(id, productId, current, 1);

            return View("Cart", pbs.GetUsersCart(id));
        }

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult RemoveProductFromCart(int productId, int current)
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            //remove product from their cart
            pbs.AddToCart(id, productId, current, -1);

            return View("Cart", pbs.GetUsersCart(id));
        }

        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult SetCartAmount(int productId, int newAmount)
        {
            //get user id
            int id = (int)HttpContext.Session.GetInt32("userId");

            //remove product from their cart
            pbs.AddToCart(id, productId, newAmount, 0);

            return View("Cart", pbs.GetUsersCart(id));
        }
    }
}
