using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    /// <summary>
    /// This class controller handles login and logout processes.
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Routing path for initial login page
        /// </summary>
        /// <returns>A login view</returns>
        [CustomAuthorization(LogoutRequired = true)]
        public IActionResult Index()
        {
            return View("Login");
        }

        /// <summary>
        /// Process a login attempt and routes to either success or failure pages.
        /// </summary>
        /// <param name="user">The user credentials to check validation on</param>
        /// <returns></returns>
        [CustomAuthorization(LogoutRequired = true)]
        [LoginActionFilter]
        public IActionResult ProcessLogin(User user)
        {
            //check for any remaining errors in the form
            if (ModelState["Email"].Errors.Any() && ModelState["Password"].Errors.Any())
            {
                //return to the form with errors
                return View("Login", new User());
            }

            ////////////////validate login here
            HttpContext.Session.SetInt32("userId", 1);
            //HttpContext.Session.SetString("userName", user.FirstName);

            return View("LoginSuccess", user);

            //return with errors
            return View("Login", user);
        }
    }
}
