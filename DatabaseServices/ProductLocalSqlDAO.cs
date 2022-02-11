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

                    if (reader.Read())
                    {
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

                    while (reader.Read())
                    {
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

        public List<CartItemDTO> GetCartList(int userId)
        {
            List<CartItemDTO> products = new List<CartItemDTO>();

            string query = "SELECT products.ID, products.IMG, products.NAME, products.PRICE, products.DESCRIPTION, products.TAGS, carts.QUANTITY " +
                           "FROM products " +
                           "INNER JOIN carts ON products.ID = carts.PRODUCTID WHERE carts.USERID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = userId;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CartItemDTO product = new CartItemDTO();
                        product.Id = (int)reader["ID"];
                        product.Img = (string)reader["IMG"];
                        product.Name = (string)reader["NAME"];
                        product.Price = (decimal)reader["PRICE"];
                        product.Description = (string)reader["DESCRIPTION"];
                        product.Tags = Product.ParseTags((string)reader["TAGS"]);
                        product.Quantity = (int)reader["QUANTITY"];

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

                    while (reader.Read())
                    {
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

        public bool AddToCart(int userId, int productId)
        {
            bool results = false;

            string query = "UPDATE carts SET QUANTITY = QUANTITY + 1 WHERE USERID = @userId AND PRODUCTID = @productId;" +
                           "IF @@ROWCOUNT = 0 " +
                           "BEGIN " +
                           "INSERT INTO carts(USERID, PRODUCTID, QUANTITY) VALUES(@userId, @productId, 1);" +
                           "END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = userId;
                command.Parameters.Add("@productId", System.Data.SqlDbType.Int).Value = productId;

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

        public bool RemoveFromCart(int userId, int productId)
        {
            bool results = false;

            string query = "UPDATE carts SET QUANTITY = QUANTITY - 1 WHERE USERID = @userId AND PRODUCTID = @productId;" +
                           "SELECT QUANTITY FROM carts WHERE USERID = @userId AND PRODUCTID = @productId AND QUANTITY = 0;" +
                           "IF @@ROWCOUNT > 0 " +
                           "BEGIN " +
                           "DELETE FROM carts WHERE USERID = @userId AND PRODUCTID = @productId;" +
                           "END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = userId;
                command.Parameters.Add("@productId", System.Data.SqlDbType.Int).Value = productId;

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
