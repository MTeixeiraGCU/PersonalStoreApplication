using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Models
{
    /// <summary>
    /// This class model represents the information stored for a single user
    /// </summary>
    public class User
    {
        //The uniques Id for this user
        public int Id { get; set; }

        //The user's First Name
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        //The user's Last Name
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        //Uniques email for contacting this user
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address format!")]
        public string Email { get; set; }

        //Password associated with this user's account
        [Required]
        public string Password { get; set; }

        //user's address as one line of text (initially optional)
        public Address MailingAddress { get; set; }

        public User()
        {
        }

        public User(int id, string firstName, string lastName, string email, string password, Address address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            MailingAddress = address;
        }

        public override string ToString()
        {
            return "Id: " + Id + ", Email: " + Email + " Name: " + FirstName + " " + LastName;
        }
    }
}
