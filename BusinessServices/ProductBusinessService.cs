using PersonalStoreApplication.DatabaseServices;
using PersonalStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.BusinessServices
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductBusinessService
    {
        //handles database layer requests
        private IProductDAO productDAO;

        public ProductBusinessService(IProductDAO productDAO)
        {
            this.productDAO = productDAO;
        }

        public List<Product> GetAllProducts()
        {
            return productDAO.GetAll();
        }

        public List<Product> SearchForProducts(string token)
        {
            if (token.Equals(string.Empty))
                return productDAO.GetAll();

            //process the search token
            token = "%" + token + "%";

            return productDAO.SearchProducts(token);
        }

        public List<Product> GetUsersCart(int userId)
        {
            return productDAO.GetCartList(userId);
        }
    }
}
