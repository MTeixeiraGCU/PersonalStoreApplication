using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        /// <summary>
        /// Entry point for the application
        /// </summary>
        /// <returns>A view of the main home page.</returns>
        public IActionResult Index()
        {
            //check for logged in user
            if(HttpContext.Session.Keys.Contains("userId"))
            {
                string name = HttpContext.Session.GetString("userName");
                ViewBag.Message = "Welcome, " + name;
            }
            else
            {
                ViewBag.Message = "Welcome";
            }
            return View(new List<Product>());
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
