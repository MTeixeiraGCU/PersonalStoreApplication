using MySqlConnector;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.DatabaseServices
{
    /// <summary>
    /// This class handles database entries and processing for user objects
    /// </summary>
    public class UserMySqlDAO : IUserDAO
    {

        //Connection environment variables
        private static string database_server = Environment.GetEnvironmentVariable("DATABASE_SERVER_NAME");
        private static string database_userId = Environment.GetEnvironmentVariable("DATABASE_USER_ID");
        private static string database_password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        private static string database_schema = Environment.GetEnvironmentVariable("DATABASE_SCHEMA");
        private static string database_port = Environment.GetEnvironmentVariable("DATABASE_PORT");

        //Connection string to MyMySql database
        private static string connectionString = "server=" + database_server + 
                                                 ";UserId=" + database_userId + 
                                                 ";password=" + database_password + 
                                                 ";database=" + database_schema + 
                                                 ";port=" + database_port;


        public int GetIdFromEmail(string email)
        {
            int id = -1;

            string query = "SELECT * FROM users WHERE EMAIL = @email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@email", email));

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        id = Convert.ToInt32(reader.GetValue(0));
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return id;
        }

        public User Get(int id)
        {
            User user = null;

            string query = "SELECT * FROM users WHERE ID = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@id", id));

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User();
                        user.Id = id;
                        user.Role = (UserRole)reader["ROLE"];
                        user.Email = (string)reader["EMAIL"];
                        user.Password = (string)reader["PASSWORD"];
                        user.FirstName = (string)reader["FIRSTNAME"];
                        user.LastName = (string)reader["LASTNAME"];
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return user;
        }

        public bool Add(User user)
        {
            bool results = false;

            string query = "INSERT INTO users (EMAIL, PASSWORD, FIRSTNAME, LASTNAME) VALUES (@email, @password, @firstname, @lastname)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@email", user.Email));
                command.Parameters.Add(new MySqlParameter("@password", user.Password));
                command.Parameters.Add(new MySqlParameter("@firstname", user.FirstName));
                command.Parameters.Add(new MySqlParameter("@lastname", user.LastName));

                try
                {
                    connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        results = true;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return results;
        }

        public bool AddAddress(int userId, Address address)
        {
            bool results = false;

            string query = "INSERT INTO addresses (USERID, ADDRESSONE, ADDRESSTWO, CITY, STATE, ZIP, PHONENUMBER) VALUES (@userId, @addressOne, @addressTwo, @city, @state, @zip, @phoneNumber)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@userId", userId));
                command.Parameters.Add(new MySqlParameter("@addressOne", address.AddressLineOne));
                command.Parameters.Add(new MySqlParameter("@addressTwo", address.AddressLineTwo));
                command.Parameters.Add(new MySqlParameter("@city", address.City));
                command.Parameters.Add(new MySqlParameter("@state", address.State));
                command.Parameters.Add(new MySqlParameter("@zip", address.Zip));
                command.Parameters.Add(new MySqlParameter("@phoneNumber", address.PhoneNumber));

                try
                {
                    connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        results = true;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return results;
        }

        public List<Address> GetAddresses(int userId)
        {
            List<Address> addresses = new List<Address>();

            string query = "SELECT * FROM addresses WHERE USERID = @userId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@userId", userId));

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Address address = new Address();
                        address.AddressLineOne = (string)reader["ADDRESSONE"];
                        address.AddressLineTwo = (string)reader["ADDRESSTWO"];
                        address.City = (string)reader["CITY"];
                        address.State = (string)reader["STATE"];
                        address.Zip = (string)reader["ZIP"];
                        address.PhoneNumber = (string)reader["PHONENUMBER"];

                        addresses.Add(address);
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return addresses;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM users";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["ID"];
                        user.Role = (UserRole)reader["ROLE"];
                        user.Email = (string)reader["EMAIL"];
                        user.Password = (string)reader["PASSWORD"];
                        user.FirstName = (string)reader["FIRSTNAME"];
                        user.LastName = (string)reader["LASTNAME"];

                        users.Add(user);
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return users;
        }

        public bool Delete(int id)
        {
            bool results = false;

            string query = "DELETE FROM users WHERE ID = @userId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@userId", id));

                try
                {
                    connection.Open();

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        results = true;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return results;
        }
    }
}
