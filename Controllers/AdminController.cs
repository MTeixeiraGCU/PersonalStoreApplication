using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.BusinessServices;

namespace PersonalStoreApplication.Controllers
{

    /// <summary>
    /// This controller handles access and routing for administrator services.
    /// </summary>
    public class AdminController : Controller
    {
        //Injected business services.
        ProductBusinessService pbs;
        RegistrationBusinessService rbs;
        LoginBusinessService lbs;

        public AdminController(ProductBusinessService pbs, RegistrationBusinessService rbs, LoginBusinessService lbs)
        {
            this.pbs = pbs;
            this.rbs = rbs;
            this.lbs = lbs;
        }

        /// <summary>
        /// This is the routing for the initial panel page for administrator access.
        /// </summary>
        /// <returns>A view of the landing page for administrator access.</returns>
        [CustomAuthorization(AdminRequired = true)]
        public IActionResult Index()
        {
            return View("Panel");
        }
        
        /// <summary>
        /// This method routes to product list page for editing products.
        /// </summary>
        /// <returns>A view containing a list of products to edit.</returns>
        [CustomAuthorization(AdminRequired = true)]
        public IActionResult EditProducts()
        {
            return View(pbs.GetAllProducts());
        }

        [CustomAuthorization(AdminRequired = true)]
        public JsonResult DeleteProduct(int productId)
        {
            //remove product here
            return Json(pbs.DeleteProduct(productId));
        }

        [CustomAuthorization(AdminRequired = true)]
        public JsonResult ProcessNewProduct(Models.Product product)
        {
            //add product here
            return Json(pbs.AddProduct(product));
        }

        /// <summary>
        /// This method routes to user list editing.
        /// </summary>
        /// <returns>A view containing all the users registered to the application.</returns>
        [CustomAuthorization(AdminRequired = true)]
        public IActionResult EditUsers()
        {
            return View(lbs.GetAllUsers());
        }

        [CustomAuthorization(AdminRequired = true)]
        public JsonResult DeleteUser(int id)
        {
            //remove user here
            return Json(rbs.UnregisterUser(id));
        }

        [CustomAuthorization(AdminRequired = true)]
        public JsonResult GetProductDetails(int productId)
        {
            return Json(pbs.GetProduct(productId));
        }
    }
}
