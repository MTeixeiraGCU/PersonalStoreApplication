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
        /// This method searches the persistence layer for the matching requirements given
        /// </summary>
        /// <param name="name">The name to search the database for.</param>
        /// <returns>A list of all the products found that match the search criteria.</returns>
        public List<Product> SearchProducts(string name);

        public List<Product> GetCartList(int userId);
    }
}
