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

        public bool UpdateCart(int userId, int productId, int quantity)
        {
            bool results = false;

            string query = "UPDATE carts SET QUANTITY = @quantity WHERE USERID = @userId AND PRODUCTID = @productId;" +
                           "IF @@ROWCOUNT = 0 " +
                           "BEGIN " +
                           "INSERT INTO carts(USERID, PRODUCTID, QUANTITY) VALUES(@userId, @productId, @quantity);" +
                           "END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = userId;
                command.Parameters.Add("@productId", System.Data.SqlDbType.Int).Value = productId;
                command.Parameters.Add("@quantity", System.Data.SqlDbType.Int).Value = quantity;

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

        public bool DeleteFromCart(int userId, int productId)
        {
            bool results = false;

            string query = "DELETE FROM carts WHERE USERID = @userId AND PRODUCTID = @productId;";

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

        public int AddProduct(Product product)
        {
            int results = -1;

            string query = "INSERT INTO products (IMG, NAME, PRICE, DESCRIPTION, TAGS) VALUES (@img, @name, @price, @description, @tags)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@img", System.Data.SqlDbType.NVarChar, 40).Value = product.Img;
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 40).Value = product.Name;
                command.Parameters.Add("@price", System.Data.SqlDbType.Decimal, 10).Value = product.Price;
                command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar, 40).Value = product.Description;
                command.Parameters.Add("@tags", System.Data.SqlDbType.NVarChar, 40).Value = Product.TagsToString(product.Tags);

                try
                {
                    connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        command = new SqlCommand("SELECT MAX(ID) FROM products", connection);
                        results = (int)command.ExecuteScalar();
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

        public bool DeleteProduct(int productId)
        {
            bool results = false;

            string query = "DELETE FROM products WHERE ID = @productId;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

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

        public bool Update(Product product)
        {
            bool results = false;

            string query = "UPDATE products SET IMG = @img, NAME = @name, PRICE = @price, DESCRIPTION = @description, TAGS = @tags WHERE ID = @productId;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@productId", System.Data.SqlDbType.Int).Value = product.Id;
                command.Parameters.Add("@img", System.Data.SqlDbType.NVarChar, 40).Value = product.Img;
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 40).Value = product.Name;
                command.Parameters.Add("@price", System.Data.SqlDbType.Decimal, 10).Value = product.Price;
                command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar, 40).Value = product.Description;
                command.Parameters.Add("@tags", System.Data.SqlDbType.NVarChar, 40).Value = Product.TagsToString(product.Tags);

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
