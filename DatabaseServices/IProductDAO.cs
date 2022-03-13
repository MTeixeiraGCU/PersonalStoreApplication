using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.DatabaseServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductDAO
    {
        /// <summary>
        /// This method gets all the current products from the persistence layer and returns them as a list.
        /// </summary>
        /// <returns>A complete list of all available products.</returns>
        public List<Product> GetAll();

        /// <summary>
        /// This method takes in a single product id and returns the product that matches it.
        /// </summary>
        /// <param name="id">The id to search the products for.</param>
        /// <returns>The found product matching the given id, will return null if no product exists.</returns>
        public Product Get(int id);

        /// <summary>
        /// This method updates the given product information in the database.
        /// </summary>
        /// <param name="product">The product information to use for the update.</param>
        /// <returns>true if the product was updated, false otherwise.</returns>
        public bool Update(Product product);

        /// <summary>
        /// This method searches the persistence layer for the matching requirements given
        /// </summary>
        /// <param name="name">The name to search the database for.</param>
        /// <returns>A list of all the products found that match the search criteria.</returns>
        public List<Product> SearchProducts(string name);

        /// <summary>
        /// Gets the list of product items and quantities from the persistence layer owned by the given user id.
        /// </summary>
        /// <param name="userId">The id of the user whose items should be complied.</param>
        /// <returns>A list of products attached to a quantity for each.</returns>
        public List<CartItemDTO> GetCartList(int userId);

        /// <summary>
        /// This method takes in a user id and product id then adds a single product to the users cart.
        /// </summary>
        /// <param name="userId">User's id to add the product to.</param>
        /// <param name="productId">Product id for the item to add.</param>
        /// <param name="quantity"></param>
        /// <returns>True if the item was added, false otherwise.</returns>
        public bool UpdateCart(int userId, int productId, int quantity);

        /// <summary>
        /// This method removes a product from the user's cart.
        /// </summary>
        /// <param name="userId">The user id for the cart to update.</param>
        /// <param name="productId">The product id of the item to remove from their cart.</param>
        /// <returns>true if the product was removed, false otherwise.</returns>
        public bool DeleteFromCart(int userId, int productId);

        /// <summary>
        /// This method adds a product to the persistence layer
        /// </summary>
        /// <param name="product">Filled out product information to add to the database.</param>
        /// <returns>true if the product was added, false otherwise.</returns>
        public int AddProduct(Product product);

        /// <summary>
        /// This method removes a product from the persistence layer.
        /// </summary>
        /// <param name="productId">The id of the product to remove from the database.</param>
        /// <returns>true if the product was removed, false otherwise.</returns>
        public bool DeleteProduct(int productId);
    }
}
