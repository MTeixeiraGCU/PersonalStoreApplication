using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.DatabaseServices
{
    /// <summary>
    /// Implementation of the Product DAO interface for the local visual studio database.
    /// </summary>
    public class ProductLocalSqlDAO : IProductDAO
    {
        //Connection string to local VS created MySQL database
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonalStoreApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Product Get(int id)
        {
            Product product = null;

            string query = "SELECT * FROM products WHERE ID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        product = new Product();
                        product.Id = id;
                        product.Img = (string)reader["IMG"];
                        product.Name = (string)reader["NAME"];
                        product.Price = (decimal)reader["PRICE"];
                        product.Description = (string)reader["DESCRIPTION"];
                        product.Tags = Product.ParseTags((string)reader["TAGS"]);
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            string query = "SELECT * FROM products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.HasRows)
                    {
                        reader.Read();

                        Product product = new Product();
                        product.Id = (int)reader["ID"];
                        product.Img = (string)reader["IMG"];
                        product.Name = (string)reader["NAME"];
                        product.Price = (decimal)reader["PRICE"];
                        product.Description = (string)reader["DESCRIPTION"];
                        product.Tags = Product.ParseTags((string)reader["TAGS"]);

                        products.Add(product);
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return products;
        }

        public List<Product> SearchProducts(string name)
        {
            List<Product> products = new List<Product>();

            string query = "SELECT * FROM products WHERE NAME LIKE @name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 40).Value = name;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.HasRows)
                    {
                        reader.Read();

                        Product product = new Product();
                        product.Id = (int)reader["ID"];
                        product.Img = (string)reader["IMG"];
                        product.Name = (string)reader["NAME"];
                        product.Price = (decimal)reader["PRICE"];
                        product.Description = (string)reader["DESCRIPTION"];
                        product.Tags = Product.ParseTags((string)reader["TAGS"]);

                        products.Add(product);
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return products;
        }
    }
}
