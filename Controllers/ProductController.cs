using Microsoft.AspNetCore.Mvc;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult ProcessSearch(string token)
        {
            return View("Index", "Home");
        }
    }
}
