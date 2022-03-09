using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.DatabaseServices
{
    /// <summary>
    /// This interface represents access to the persistence layer for adding and validating a user
    /// </summary>
    public interface IUserDAO
    {
        /// <summary>
        /// This method finds the user's unique Id from their given email.
        /// </summary>
        /// <param name="email">The email that the user chose during registration.</param>
        /// <returns>The found Id that matches the user's unique email. returns -1 if no entries were found.</returns>
        public int GetIdFromEmail(string email);

        /// <summary>
        /// This method gets a single user by their unique Id.
        /// </summary>
        /// <param name="id">The id to check the database for.</param>
        /// <returns>The found user object found from the given id. returns null if no users were found.</returns>
        public User Get(int id);

        /// <summary>
        /// The method gets all of the registered users in the applications persistence layer.
        /// </summary>
        /// <returns>A complied list of all registered users.</returns>
        public List<User> GetAll();

        /// <summary>
        /// This method adds a user to the database.
        /// </summary>
        /// <param name="user">The user object with the information to add.</param>
        /// <returns>Boolean value that represents whether the user was successfully added. true if they were added, false otherwise.</returns>
        public bool Add(User user);

        /// <summary>
        /// This method adds an address to the given user.
        /// </summary>
        /// <param name="userId">The id of the user to add an address for.</param>
        /// <param name="address">The address to add.</param>
        /// <returns>true if the address was added, false otherwise.</returns>
        public bool AddAddress(int userId, Address address);

        /// <summary>
        /// This method gets all the addresses attached to the given user.
        /// </summary>
        /// <param name="userId">The id to search address for.</param>
        /// <returns>A compiled list of addresses from the persistence layer.</returns>
        public List<Address> GetAddresses(int userId);
    }
}
