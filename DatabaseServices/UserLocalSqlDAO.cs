﻿using PersonalStoreApplication.Models;
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


        public int GetIdFromEmail(string email)
        {
            int id = -1;

            string query = "SELECT * FROM users WHERE EMAIL = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 40).Value = email;

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

        public User Get(int id)
        {
            User user = null;

            string query = "SELECT * FROM users WHERE ID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User();
                        user.Id = id;
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 40).Value = user.Email;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 40).Value = user.Password;
                command.Parameters.Add("@firstname", System.Data.SqlDbType.NVarChar, 40).Value = user.FirstName;
                command.Parameters.Add("@lastname", System.Data.SqlDbType.NVarChar, 40).Value = user.LastName;

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.NVarChar, 40).Value = userId;
                command.Parameters.Add("@addressOne", System.Data.SqlDbType.NVarChar, 40).Value = address.AddressLineOne;
                command.Parameters.Add("@addressTwo", System.Data.SqlDbType.NVarChar, 40).Value = address.AddressLineTwo;
                command.Parameters.Add("@city", System.Data.SqlDbType.NVarChar, 40).Value = address.City;
                command.Parameters.Add("@state", System.Data.SqlDbType.NVarChar, 40).Value = address.State;
                command.Parameters.Add("@zip", System.Data.SqlDbType.NVarChar, 40).Value = address.Zip;
                command.Parameters.Add("@phoneNumber", System.Data.SqlDbType.NVarChar, 40).Value = address.PhoneNumber;

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.NVarChar, 40).Value = userId;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

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
    }
}
