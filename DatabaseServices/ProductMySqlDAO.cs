using MySqlConnector;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.DatabaseServices
{
    /// <summary>
    /// Implementation of the Product DAO interface for the local visual studio database.
    /// </summary>
    public class ProductMySqlDAO : IProductDAO
    {
        //Connection environment variables
        private static string database_server = Environment.GetEnvironmentVariable("DATABASE_SERVER_NAME");
        private static string database_userId = Environment.GetEnvironmentVariable("DATABASE_USER_ID");
        private static string database_password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        private static string database_schema = Environment.GetEnvironmentVariable("DATABASE_SCHEMA");
        private static string database_port = Environment.GetEnvironmentVariable("DATABASE_PORT");

        //Connection string to MyMyMySql database
        private static string connectionString = "server=" + database_server +
                                                 ";UserId=" + database_userId +
                                                 ";password=" + database_password +
                                                 ";database=" + database_schema +
                                                 ";port=" + database_port;

        public Product Get(int id)
        {
            Product product = null;

            string query = "SELECT * FROM products WHERE ID = @id";

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                };
            }

            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            string query = "SELECT * FROM products";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
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

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@id", userId));

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                };
            }

            return products;
        }

        public List<Product> SearchProducts(string name)
        {
            List<Product> products = new List<Product>();

            string query = "SELECT * FROM products WHERE NAME LIKE @name";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@name", name));

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                };
            }

            return products;
        }

        public bool UpdateCart(int userId, int productId, int quantity)
        {
            bool results = false;

            string query = "UPDATE carts SET QUANTITY = @quantity WHERE USERID = @userId AND PRODUCTID = @productId;" +
                           "IF @@ROWCOUNT = 0 " +
                           "BEGIN " +
                           "INSERT INTO carts(USERID, PRODUCTID, QUANTITY) VALUES(@userId, @productId, @quantity);" +
                           "END";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@userId", userId));
                command.Parameters.Add(new MySqlParameter("@productId", productId));
                command.Parameters.Add(new MySqlParameter("@quantity", quantity));

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                };
            }

            return results;
        }

        public bool DeleteFromCart(int userId, int productId)
        {
            bool results = false;

            string query = "DELETE FROM carts WHERE USERID = @userId AND PRODUCTID = @productId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@userId", userId));
                command.Parameters.Add(new MySqlParameter("@productId", productId));

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                };
            }

            return results;
        }

        public int AddProduct(Product product)
        {
            int results = -1;

            string query = "INSERT INTO products (IMG, NAME, PRICE, DESCRIPTION, TAGS) VALUES (@img, @name, @price, @description, @tags)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@img", product.Img));
                command.Parameters.Add(new MySqlParameter("@name", product.Name));
                command.Parameters.Add(new MySqlParameter("@price", product.Price));
                command.Parameters.Add(new MySqlParameter("@description", product.Description));
                command.Parameters.Add(new MySqlParameter("@tags", Product.TagsToString(product.Tags)));

                try
                {
                    connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        command = new MySqlCommand("SELECT MAX(ID) FROM products", connection);
                        results = (int)command.ExecuteScalar();
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }

            return results;
        }

        public bool DeleteProduct(int productId)
        {
            bool results = false;

            string query = "DELETE FROM products WHERE ID = @productId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@productId", productId));

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                };
            }

            return results;
        }

        public bool Update(Product product)
        {
            bool results = false;

            string query = "UPDATE products SET IMG = @img, NAME = @name, PRICE = @price, DESCRIPTION = @description, TAGS = @tags WHERE ID = @productId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add(new MySqlParameter("@productId", product.Id));
                command.Parameters.Add(new MySqlParameter("@img", product.Img));
                command.Parameters.Add(new MySqlParameter("@name", product.Name));
                command.Parameters.Add(new MySqlParameter("@price", product.Price));
                command.Parameters.Add(new MySqlParameter("@description", product.Description));
                command.Parameters.Add(new MySqlParameter("@tags", Product.TagsToString(product.Tags)));

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
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                };
            }

            return results;
        }
    }
}
