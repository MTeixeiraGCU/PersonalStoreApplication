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
            //get the valid user from the email
            int id = userDAO.GetIdFromEmail(user.Email);
            User validUser = userDAO.Get(id);

            //check if user exists and if the passwords match
            if (validUser == null || !(validUser.Password.Equals(user.Password)))
                return -1;

            //user was validated
            return validUser.Id;
        }

        /// <summary>
        /// This method gets the user from the database layer from the given id.
        /// </summary>
        /// <param name="id">The user id to search the database layer for.</param>
        /// <returns>A complete User object if found, null if there is no user attached to the id given.</returns>
        public User GetUser(int id)
        {
            return userDAO.Get(id);
        }
    }
}
