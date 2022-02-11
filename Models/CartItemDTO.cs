using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Models
{
    /// <summary>
    /// This class represents a store product item within a users personal cart.
    /// </summary>
    public class CartItemDTO
    {
        //Unique Identifier for Product
        public int Id { get; set; }

        //Full product image string with attached file type
        public string Img { get; set; }

        //Name of the product
        public string Name { get; set; }

        //Cost of the product in U.S. Dollars
        public decimal Price { get; set; }

        //Short description of the product
        public string Description { get; set; }

        //List of all relevant tags describing this product
        public List<string> Tags { get; set; }

        //Describes the number of units of this product that should be in the cart.
        public int Quantity { get; set; }

        public CartItemDTO()
        {
        }

        public static List<string> ParseTags(string tags)
        {
            var tagArray = tags.Split(',');
            return tagArray.ToList();
        }
    }
}
