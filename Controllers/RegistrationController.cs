using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    /// <summary>
    /// This class controller is designed to process routing for the registration module
    /// </summary>
    [CustomAuthorization(LogoutRequired = true)]
    public class RegistrationController : Controller
    {
        /*private RegistrationBusinessService rbs;

        public RegistrationController(RegistrationBusinessService rbs)
        {
            this.rbs = rbs;
        }*/

        /// <summary>
        /// Initial registration module entry point route.
        /// </summary>
        /// <returns>A view of the initial form for user registration.</returns>
        [CustomAuthorization(LogoutRequired = true)]
        public IActionResult Index()
        {
            return View("Register", new User());
        }

        /// <summary>
        /// This routing path attempts to process a user's registration information and add them into the system.
        /// </summary>
        /// <param name="user">The user information received from the request to be processed.</param>
        /// <returns>A view based on successful registration or returns to the registration form with errors.</returns>
        public IActionResult ProcessRegistration(User user)
        {
            //Check for duplicate email
            /*if (!rbs.CheckEmailAvailability(user.Email))
            {
                ModelState.AddModelError("Email", "That email has been used for another account already!");
            }*/

            //check for any remaining errors in the form
            if (!ModelState.IsValid)
            {
                //reset password
                user.Password = null;

                //return to the form with errors
                return View("Register", user);
            }

            //register user
            if (true)//rbs.RegisterUser(user))
            {
                return View("RegistrationSuccess", user);
            }
            else
            {
                return View("RegistrationFailure", user);
            }
        }
    }
}