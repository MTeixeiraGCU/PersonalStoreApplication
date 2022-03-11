using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Models
{
    /// <summary>
    /// This class represents a store product with name, cost, and short description along with any tags related to this product.
    /// </summary>
    public class Product
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

        public Product()
        {
        }

        public Product(int id, string img, string name, decimal price, string description, List<string> tags)
        {
            Id = id;
            Img = img;
            Name = name;
            Price = price;
            Description = description;
            Tags = tags;
        }

        public override string ToString()
        {
            return "Id: " + Id + ", Name: " + Name + ", Cost: $" + string.Format("{0:C}", Price); 
        }

        public static List<string> ParseTags(string tags)
        {
            var tagArray = tags.Split(',');
            return tagArray.ToList();
        }

        public static string TagsToString(List<string> tags)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string tag in tags)
            {
                sb.Append(tag + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
