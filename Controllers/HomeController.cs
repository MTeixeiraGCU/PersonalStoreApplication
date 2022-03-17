using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalStoreApplication.BusinessServices;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    /// <summary>
    /// This class controller handles routing for all initial page landings.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        //handles product logic and database queries
        private ProductBusinessService pbs;

        public HomeController(ProductBusinessService productBusinessService, ILogger<HomeController> logger)
        {
            pbs = productBusinessService;
            _logger = logger;
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
            return View(pbs.GetAllProducts());
        }

        /// <summary>
        /// This method routes to the About page view.
        /// </summary>
        /// <returns>A view containing the loaded about page.</returns>
        public IActionResult About()
        {
            _logger.LogInformation("About page was accessed!");
            return View();
        }

        /// <summary>
        /// This method handles various page errors and routing.
        /// </summary>
        /// <returns>A view of produced from the given error parameters.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
