using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster
{
    class Vehicle
    {
        public int Capacity { get; set; }//Capacity of the vehicle 
        public IReadOnlyCollection<Product> Trunk { get; set; }//collections of products
        private bool isFull;
        private bool isEmpty;
        public Vehicle(int Capacity)
        {
            this.Capacity = Capacity;
        }

        /*To check whether the vehicle is full or not*/
        public bool IsFull()
        {
            double totalWeight = 0;
            foreach (Product p in Trunk)
            {
                totalWeight = totalWeight + p.Weight;
            }
            if (totalWeight < Capacity)
            {
                isFull = false;
                return isFull;
            }
            else
            {
                isFull = true;
                return isFull;
            }
        }

        /*To check whether the vehicle is empty or not*/
        public bool IsEmpty()
        {
            int checkTrunk = Trunk.Count;
            if (checkTrunk > 0)
            {
                isEmpty = false;
                return isEmpty;
            }
            else
            {
                isEmpty = true;
                return isEmpty;
            }
        }

        /*If the vehicle is full the following method throws an exception
         * If the vehicle is empty then the product is added to the Trunk*/
        public void LoadProduct(Product product)
        {
           if (IsFull())
            {
                throw new InvalidOperationException("Vehicle is full");
            }
            else
            {
                Trunk.Append(product);
            }
        }


        /*If the vehicle is empty the following method throws an exception
        * If the vehicle is not full then the last product in the Trunk is removed and return it*/
        public Product Unload()
        {
            int checkTrunk = Trunk.Count;
            if (IsEmpty())
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }
            else if (checkTrunk == 1)
            {
                Product unloadProd = Trunk.ElementAt(checkTrunk);
                Trunk = null;
                return unloadProd;
            }
            else
            {
                Product unloadProd = Trunk.ElementAt(checkTrunk);
                Trunk = (IReadOnlyCollection<Product>)Trunk.Take(checkTrunk - 1);
                return unloadProd;
            }
        }
    }
}
