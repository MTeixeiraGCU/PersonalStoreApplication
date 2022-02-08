using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.DatabaseServices
{
    /// <summary>
    /// This class handles database entries and processing for user objects
    /// </summary>
    public class UserLocalSqlDAO : IUserDAO
    {
        //Connection string to local VS created MySQL database
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonalStoreApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public bool FindUserByEmailAndPassword(string email, string password)
        {
            bool success = false;

            string query = "SELECT * FROM users WHERE EMAIL = @email and PASSWORD = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.NChar, 40).Value = email;
                command.Parameters.Add("@password", System.Data.SqlDbType.NChar, 40).Value = password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return success;
        }

        public int GetIdFromEmail(string email)
        {
            int id = -1;

            string query = "SELECT * FROM users WHERE EMAIL = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 40).Value = email;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

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

        public bool Add(User user)
        {
            bool success = false;

            string query = "INSERT INTO users (EMAIL, PASSWORD, FIRSTNAME, LASTNAME) VALUES (@email, @password, @firstname, @lastname)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 40).Value = user.Email;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 40).Value = user.Password;
                command.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 40).Value = user.FirstName;
                command.Parameters.Add("@lastname", System.Data.SqlDbType.NVarChar, 40).Value = user.LastName;

                try
                {
                    connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        success = true;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;
        }
    }
}
