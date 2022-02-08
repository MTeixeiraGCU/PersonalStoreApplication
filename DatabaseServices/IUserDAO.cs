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
        /// This method retrieves a user's complete information from the given user name and password.
        /// </summary>
        /// <param name="email">Unique user name to search for in the database</param>
        /// <param name="password">Associated password to validate user with.</param>
        /// <returns>Boolean value of successful location of user credentials. true if they were found, false otherwise.</returns>
        public bool FindUserByEmailAndPassword(string email, string password);

        /// <summary>
        /// This method finds the user's unique Id from their given email.
        /// </summary>
        /// <param name="email">The email that the user chose during registration.</param>
        /// <returns>The found Id that matches the user's unique email. returns -1 if no entries were found.</returns>
        public int GetIdFromEmail(string email);

        /// <summary>
        /// This method adds a user to the database.
        /// </summary>
        /// <param name="user">The user object with the information to add.</param>
        /// <returns>Boolean value that represents whether the user was successfully added. true if they were added, false otherwise.</returns>
        public bool Add(User user);
    }
}
