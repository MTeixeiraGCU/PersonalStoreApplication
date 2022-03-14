using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace PersonalStoreApplication.Controllers
{
    /// <summary>
    /// This class is a login authorization filter. It is used to detect for a valid login situation.
    /// </summary>
    internal class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public bool LogoutRequired { get; set; }
        public bool AdminRequired { get; set; }

        public const int MAX_ATTEMPTS = 10;

        /// <summary>
        /// This method checks for a valid user id in the sessions variables. If one does not exist, it redirects to the login page.
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            int? userId = context.HttpContext.Session.GetInt32("userId");
            int? role = context.HttpContext.Session.GetInt32("userRole");
            int? attemptStatus = context.HttpContext.Session.GetInt32("attemptStatus");
            bool isAdmin = false;

            //check for lockout status
            if(attemptStatus != null && (int)attemptStatus >= MAX_ATTEMPTS)
            {
                context.Result = new RedirectResult("/Login/LockedOut");
            }

            //check administrator status
            if(role != null)
            {
                if((Models.UserRole)role == Models.UserRole.Administrator)
                    isAdmin = true;
            }

            if (userId == null && !LogoutRequired)
                context.Result = new RedirectResult("/Login");
            else if (userId != null && LogoutRequired)
                context.Result = new RedirectResult("/Login/Logout?message=You must log out before you can do that!");
            else if (userId != null && AdminRequired && !isAdmin)
                context.Result = new RedirectResult("/Login/Logout?message=You must log in as an administrator to process that action!");
        }
    }
}
