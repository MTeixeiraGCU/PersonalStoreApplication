using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Controllers
{
    /// <summary>
    /// This class is used to log user log-ins and log-outs.
    /// </summary>
    internal class LoginActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// This method gets called after a login attempt has been processed and completed. 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

            //Logs after execution
        }

        /// <summary>
        /// This method gets called just before the login process begins.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Logs during execution
        }
    }
}
