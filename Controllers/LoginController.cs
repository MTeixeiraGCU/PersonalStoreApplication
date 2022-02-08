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
    /// This class controller handles login and logout processes.
    /// </summary>
    public class LoginController : Controller
    {
        //handles accessing logic and database queries.
        private LoginBusinessService lbs;

        //Default constructor, LoginBusinessService must be injected at creation
        public LoginController(LoginBusinessService lbs)
        {
            this.lbs = lbs;
        }

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

            user.Id = lbs.ValidateLogin(user);

            if (user.Id != -1)
            {
                //setup session variables
                HttpContext.Session.SetInt32("userId", user.Id);
                HttpContext.Session.SetString("userName", user.FirstName);

                return View("LoginSuccess", user);
            }
            else
            {
                //return with errors
                ModelState.AddModelError(string.Empty, "Email and Password combination did not match.");
                return View("Login", user);
            }
        }

        /// <summary>
        /// This method routes to logout the logout page
        /// </summary>
        /// <returns>A view to the logout page view</returns>
        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult Logout()
        {
            return View();
        }

        /// <summary>
        /// This is a helper method to create a logged out state.
        /// </summary>
        /// <returns>True if there was a suer and they were logged out. False if there was no user to logout.</returns>
        [CustomAuthorization(LogoutRequired = false)]
        public IActionResult ProcessLogout()
        {

            if (HttpContext.Session.GetInt32("userId") != null)
            {

                //remove session variables
                HttpContext.Session.Remove("userId");
                HttpContext.Session.Remove("userName");
            }
            return View("Login");
        }
    }
}
