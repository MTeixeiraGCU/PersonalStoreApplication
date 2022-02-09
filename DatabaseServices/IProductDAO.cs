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
    }
}
