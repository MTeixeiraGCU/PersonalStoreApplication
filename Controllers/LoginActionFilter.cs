using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    internal class LoginActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

            User user = (User)((Controller)context.Controller).ViewData.Model;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Processing login info here.
        }
    }
}
