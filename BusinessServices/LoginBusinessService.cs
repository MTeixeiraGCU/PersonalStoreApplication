using PersonalStoreApplication.DatabaseServices;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.BusinessServices
{
    /// <summary>
    /// This class service is designed to handle logic for user login and validation.
    /// </summary>
    public class LoginBusinessService
    {
        //Database service object for users
        private IUserDAO userDAO;

        public LoginBusinessService(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        /// <summary>
        /// This method takes in a user object and attempts to validate their credentials against the database.
        /// </summary>
        /// <param name="user">A user object containing the login information to process.</param>
        /// <returns>Integer value of the logged in user. Will return -1 if the log in failed.</returns>
        public int ValidateLogin(User user)
        {
            if (userDAO.FindUserByEmailAndPassword(user.Email, user.Password))
            {
                return userDAO.GetIdFromEmail(user.Email);
            }
            return -1;
        }
    }
}
