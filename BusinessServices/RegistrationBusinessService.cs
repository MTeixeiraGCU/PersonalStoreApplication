using PersonalStoreApplication.DatabaseServices;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.BusinessServices
{
    /// <summary>
    /// This business service class is designed to handle processing logic for user registration.
    /// </summary>
    public class RegistrationBusinessService
    {
        //Database service object for users
        private IUserDAO userDAO;

        public RegistrationBusinessService(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        /// <summary>
        /// Takes in a user object and attempts to add them to the database. Does not create login status.
        /// </summary>
        /// <param name="user">The complete user object to add to the database.</param>
        /// <returns>true if the user was added, false otherwise.</returns>
        public bool RegisterUser(User user)
        {
            return userDAO.Add(user);
        }

        /// <summary>
        /// This method checks for a duplicate user email in the system.
        /// </summary>
        /// <param name="email">The email to match the database against.</param>
        /// <returns>true if a user has registered the given email already, false otherwise</returns>
        public bool CheckEmailAvailability(string email)
        {
            if (userDAO.GetIdFromEmail(email) == -1)
                return true;
            return false;
        }
    }
}
