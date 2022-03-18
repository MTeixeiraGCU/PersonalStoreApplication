using Microsoft.Extensions.Logging;
using PersonalStoreApplication.DatabaseServices;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.BusinessServices
{
    /// <summary>
    /// This class handles business logic for dealing with Product requests.
    /// </summary>
    public class ProductBusinessService
    {
        //handles database layer requests
        private IProductDAO productDAO;

        private readonly ILogger _logger;

        public ProductBusinessService(IProductDAO productDAO, ILogger<ProductBusinessService> logger)
        {
            this.productDAO = productDAO;
            _logger = logger;
        }

        /// <summary>
        /// This method gets a list of all products from the persistence layer.
        /// </summary>
        /// <returns>A list compiled of all the products available.</returns>
        public List<Product> GetAllProducts()
        {
            var products = productDAO.GetAll();

            foreach(var product in products)
            {
                product.Img = findProductImage(product);
            }

            return products;
        }

        /// <summary>
        /// This method finds a single product based on an id.
        /// </summary>
        /// <param name="productId">The given id to search for.</param>
        /// <returns>A product that matches the given id.</returns>
        public Product GetProduct(int productId)
        {
            var product = productDAO.Get(productId);

            product.Img = findProductImage(product);

            return product;
        }

        /// <summary>
        /// This method takes in a product and attempts to update it in the persistence layer.
        /// </summary>
        /// <param name="product">The product information to update.</param>
        /// <returns>true if the product was updated, false otherwise.</returns>
        public bool UpdateProduct(Product product)
        {
            return productDAO.Update(product);
        }

        /// <summary>
        /// This method searches for matching products based on name and the given token.
        /// </summary>
        /// <param name="token">The token to search names in the persistence layer with.</param>
        /// <returns>A list of any products whose name matches the given search token.</returns>
        public List<Product> SearchForProducts(string token)
        {
            if (token.Equals(string.Empty))
                return productDAO.GetAll();

            //process the search token
            token = "%" + token + "%";

            var products = productDAO.SearchProducts(token);

            foreach (var product in products)
            {
                product.Img = findProductImage(product);
            }

            return products;
        }

        /// <summary>
        /// This method gets all the products listed inside a given users cart.
        /// </summary>
        /// <param name="userId">The user id to identify which cart to retrieve.</param>
        /// <returns>A list of all products in the users cart.</returns>
        public List<CartItemDTO> GetUsersCart(int userId)
        {
            return productDAO.GetCartList(userId);
        }

        /// <summary>
        /// This method adds and removes products from the given user's cart.
        /// </summary>
        /// <param name="userId">The user id to add products with.</param>
        /// <param name="productId">The product id to add into their cart.</param>
        /// <param name="current">The current quantity of the given product in the cart.</param>
        /// <param name="amount">The amount to add into the cart.</param>
        /// <returns></returns>
        public bool AddToCart(int userId, int productId, int current, int amount)
        {
            int quantity = current + amount;
            if (quantity <= 0)
                return productDAO.DeleteFromCart(userId, productId);
            return productDAO.UpdateCart(userId, productId, quantity);
        }

        /// <summary>
        /// This method attempts to add a new product to the data layer.
        /// </summary>
        /// <param name="product">The product information to add to the database.</param>
        /// <returns>true if the product was added, false otherwise.</returns>
        public int AddProduct(Product product)
        {
            return productDAO.AddProduct(product);
        }

        /// <summary>
        /// This method queries the data layer to remove a product from the database.
        /// </summary>
        /// <param name="productId">The id of the product to remove.</param>
        /// <returns>true if the product was removed, false otherwise.</returns>
        public bool DeleteProduct(int productId)
        {
            return productDAO.DeleteProduct(productId);
        }

        private string findProductImage(Product product)
        {
            if(System.IO.File.Exists("./wwwroot/img/" + product.Img))
            {
                return "/img/" + product.Img;
            }

            string link = "https://loremflickr.com/160/120/" + product.Name.Split(" ")[0] + "?lock=" + product.Id;

            return link;
        }
    }
}
