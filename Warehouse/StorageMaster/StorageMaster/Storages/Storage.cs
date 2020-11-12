using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StorageMaster
{
    class Storage
    {
        private string name;     // storage's name
        private int capacity;    // maximum weight of products the storage can handle
        private int weight;         // weight used in the storage
        private int garageSlots; // number of slots available in the storage's garage
        private Vehicle[] vehicles;  // vehicles in the storage
        private List<Product> products;  // products in the storage

        public string Name { get { return this.name; } }
        public int Capacity { get { return this.capacity; } }
        public int GarageSlots { get { return this.garageSlots; } }

        public bool IsFull()
        {
            if (weight >= capacity) // is full in case the weight is equal then the capacity. >= is used to be safe
                return true;
            else
                return false;
        }
        public IReadOnlyCollection<Vehicle> Garage()
        {
            return this.vehicles;
        }
        public IReadOnlyCollection<Product> Products()
        {
            return this.products;
        }

        public Storage(string name, int capacity, int garageSlots, params Vehicle[] vehicles)
        {
            this.name = name;
            this.capacity = capacity;
            this.garageSlots = garageSlots;

            this.vehicles = new Vehicle[capacity];
            for(int i = 0; i < vehicles.Length; i++) // pupulates the vehicles internal array with the comming vehicles thru the constructor
            {
                this.vehicles[i] = vehicles[i];
            }

            products = new List<Product>(); // the warehouse is inicialized with no products inside.
        }

        public Vehicle GetVehicle(int garageSlot)
        {
            if ((garageSlot < 0) || (garageSlot >= this.garageSlots)) // out of range
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }
            if (vehicles.ElementAt<Vehicle>(garageSlot) == null) // there is no vehicle in that slot
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }
            return vehicles.ElementAt<Vehicle>(garageSlot); // returns the vehicle
        }

        public int FreeSlot()
        {
            for(int i = 0; i < this.capacity; i++)
            {
                if (vehicles.ElementAt<Vehicle>(i) == null)
                    return i;
            }
            return -1;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            this.vehicles.Append<Vehicle>(vehicle);
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            Vehicle vehicle;
            try
            {
                vehicle = this.GetVehicle(garageSlot);
            }
            catch
            {
                throw;
            }

            int freeSlot = deliveryLocation.FreeSlot();
            if (freeSlot < 0)
            {
                throw new InvalidOperationException("No room in garage!");
            }

            this.vehicles[garageSlot] = null;
            deliveryLocation.AddVehicle(vehicle);
            return freeSlot;
        }

        public int UnloadVehicle(int garageSlot)
        {
            double totalWeight = 0;

            Vehicle currentVehicle = this.vehicles.ElementAt<Vehicle>(garageSlot);
            if (currentVehicle == null)
            {
                throw new InvalidOperationException("No vehicle in that slot");
            }

            foreach (Product product in currentVehicle.Trunk)
            {
                totalWeight += product.Weight;
            }
            if (this.capacity == this.weight)
            {
                throw new InvalidOperationException("Storage is full!");
            }
            if(totalWeight+(double)this.weight > (double)this.capacity)
            {
                throw new InvalidOperationException("Storage is full!");
            }

            int totalUnloadedProducts = 0;
            while (!currentVehicle.IsEmpty())
            {
                Product unloadedProduct = currentVehicle.Unload();
                this.products.Append<Product>(unloadedProduct);
                totalUnloadedProducts++;
            }
            return totalUnloadedProducts;
        }
    }
}
