using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster
{
    //Products implicitly inherits from the Object class.
    public class Product
    {
        public Product()
        {
        }
        public double Price { get; }
        public double Weight { get; set; }
        
        //instance constructor with 2 parameters
        public Product(double price, double weight)
        {
            //checking for negative price
            if(price < 0)
            {
                throw new InvalidOperationException("Price cannot be negative!");
            }
            //checking for 0 or negative weight
            else if(weight <= 0)
            {
                throw new InvalidOperationException("Weight cannot be Zero or negative");
            }
            this.Price = price;
            this.Weight = weight;
        }
    }
}
