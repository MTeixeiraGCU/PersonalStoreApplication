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
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Process a login attempt and routes to either success or failure pages.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult ProcessLogin(User user)
        {

            ////////////////validate login here
            
            return View("LoginSuccess", user);
        }
    }
}
