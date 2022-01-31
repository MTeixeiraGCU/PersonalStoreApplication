using System;
using System.Collections.Generic;
using System.Linq;
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

        //Dollar amount for the cost of the product
        public int CostDollars { get; set; }

        //Cents amount for the product
        public int CostCents { get; set; }

        //Short description of the product
        public string Description { get; set; }

        //List of all relevant tags describing this product
        public List<string> Tags { get; set; }

        public Product()
        {
        }

        public Product(int id, string img, string name, int costDollars, int costCents, string description, List<string> tags)
        {
            Id = id;
            Img = img;
            Name = name;
            CostDollars = costDollars;
            CostCents = costCents;
            Description = description;
            Tags = tags;
        }

        public override string ToString()
        {
            return "Id: " + Id + ", Name: " + Name + ", Cost: $" + CostDollars + "." + CostCents; 
        }
    }
}
