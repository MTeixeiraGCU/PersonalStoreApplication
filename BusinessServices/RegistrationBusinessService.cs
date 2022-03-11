using PersonalStoreApplication.Controllers;
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

        /// <summary>
        /// This method attempts to add an address to the given users profile
        /// </summary>
        /// <param name="userId">the user profile id to attach the address to.</param>
        /// <param name="address">the address object to add.</param>
        /// <returns>true if the address was added to the system.</returns>
        public bool AddAddress(int userId, Address address)
        {
            return userDAO.AddAddress(userId, address);
        }

        /// <summary>
        /// This method gets all the listed addresses for the given user.
        /// </summary>
        /// <param name="userId">the user profile id to search addresses for.</param>
        /// <returns>The list of found addresses.</returns>
        public List<Address> GetAddresses(int userId)
        {
            return userDAO.GetAddresses(userId);
        }

        /// <summary>
        /// This method unregisters and removes a user from the persistence layer.
        /// </summary>
        /// <param name="id">The id of the user to remove.</param>
        /// <returns>true if the user was removed, false otherwise.</returns>
        public bool UnregisterUser(int id)
        {
            return userDAO.Delete(id);
        }
    }
}
