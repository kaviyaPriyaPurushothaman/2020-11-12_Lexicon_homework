using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster.Core
{
    class StorageMaster
    {
        Vehicle currentVehicle;
        private List<Storage> storages;     // the storage registry list
        private List<Product> productPool; //the product list in the pool of a storage


        //public string AddProduct(string type, double price)
        //{
        //    //creating new Product
        //    Product newProduct;

        //    if (type == "Ram")  //if the parameter "type" is Ram
        //    {
        //        newProduct = new Ram();  // we create new Ram 
        //        newProduct.Price = price;   //assign new price to Ram
        //        productPool.Add(newProduct);    //add the new Ram to the pool of products
        //        return $"Added {type} to pool"; //print out the result
        //    }
        //    else if (type == "Gpu") //if the parameter type is Gpu and similar on the else if methods comming below
        //    {
        //        newProduct = new Gpu();
        //        newProduct.Price = price;
        //        productPool.Add(newProduct);
        //        return $"Added {type} to pool";
        //    }
        //    else if (type == "HardDrive")
        //    {
        //        newProduct = new HardDrive();
        //        newProduct.Price = price;
        //        productPool.Add(newProduct);
        //        return $"Added {type} to pool";
        //    }
        //    else if (type == "SolidstateDrive")
        //    {
        //        newProduct = new SolidStateDrive();
        //        newProduct.Price = price;
        //        productPool.Add(newProduct);
        //        return $"Added {type} to pool";
        //    }
        //    //when no matches with the "type" parameter with the is statements
        //    throw new NotImplementedException("Invalid product type!");
        //}

        //public string RegisterStorage(string type, string name)
        //{
        //    //Creating new newStorage variable of Storage class 
        //    Storage newStorage; //
        //    if (type == "AutomatedWarehouse")   //when the type is AutomatedWarehouse..
        //    {
        //        newStorage = new AutomatedWarehouse(name);  //Creating new AutomatedWarehouse with that name
        //        storages.Add(newStorage);   //We add the new AutomatedWarhouse to the storages list
        //        return $"Registred {name}"; //we print out the new AutoWarehouse name that is added.
        //    }
        //    else if (type == "DistributionCenter")
        //    {
        //        newStorage = new DistributionCenter(name);
        //        storages.Add(newStorage);
        //        return $"Registred {name}";
        //    }
        //    else if (type == "Warehouse")
        //    {
        //        newStorage = new Warehouse(name);
        //        storages.Add(newStorage);
        //        return $"Registred {name}";
        //    }
        //    throw new NotImplementedException("Invalide storage type!");
        //}

        /*If the vehicle is found at the given garageSlot then the following method select the vehicle and return the type of the vehicle 
         * If the vehicle is not found then the function return no vehicle is selected*/

        public string SelectVehicle(string storageName, int garageSlot)
        {

            for (int i = 0; i < storages.Count; i++)// goes thru the list of all storages
            {
                if (storageName == storages.ElementAt<Storage>(i).Name)// checks the one with the right name
                {
                    currentVehicle = storages.ElementAt<Storage>(i).GetVehicle(garageSlot);//get the vehicle in the given garageSlot and stored in currentVehicle
                    return $"Selected {currentVehicle.GetType()}";
                }
            }
            return $"no vehicle is selected at {storageName}";
        }
        /* If the productNames are same with the productPool then last product with that name is removed and loaded into the current vehicle
         * If the productNames are not same with the productPool then the following method throws an exception
        */

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            bool found = false;
            int loadedProductsCount = 0;
            for (int i = 0; i < productNames.Count(); i++)//loop continues for every element in the productNames
            {

                foreach (var product in productPool)//to check every product in the productPool
                {
                    String type = product.GetType().Name;//get the type of product in productPool
                    if (productNames.ElementAt(i) == type)//compare the element of productNames and type of productPool
                    {
                        Product Loadproduct = productPool.FindLast(x => type == productNames.ElementAt(i));//to find the last product with that name in the pool 
                        currentVehicle.LoadProduct(Loadproduct);//load the current vehicle
                        loadedProductsCount++;//increment the loaded product count
                        productPool.Remove(Loadproduct);//remove the loaded product from product pool
                        found = true;//check the productName is found or not
                        break;
                    }

                }
                if (!found)//if the productName is not founded
                {
                    throw new InvalidOperationException($"{productNames.ElementAt(i)} is Out of stock!");
                }

            }
            return $"Loaded {loadedProductsCount}/{productNames.Count()} products into {currentVehicle.GetType()} ";
        }

        /*The following method throws an exception if the sourceName or destinationName does not exist in storages
         * If it is exist then the vehicle in the given sourceGarageSlot is sends to the destinationName
         */

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            Storage source = storages.Find(x => x.Name == sourceName);//to check whether the sorceName is exist or not
            Storage destination = storages.Find(x => x.Name == destinationName);//to check whether the sorceName is exist or not
            if (source == null)//if the sourceName is not exist
            {
                throw new NotImplementedException("Invalid Source Storage!");
            }
            else if (destination == null)//if the destinationName is not exist
            {
                throw new NotImplementedException("Invalid Destination Storage!");
            }
            else
            {
                currentVehicle = source.GetVehicle(sourceGarageSlot);//get the vehicle from the given sourceGarageSlot
                int destinationGarageSlot = source.SendVehicleTo(sourceGarageSlot, destination);//sends vehicle to the given destinationName
                return $"Sent {currentVehicle.GetType()} to {destinationName} (slot {destinationGarageSlot})";
            }
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            for (int i = 0; i < storages.Count; i++) // goes thru the list of all storages
            {
                if (storageName == storages.ElementAt<Storage>(i).Name) // checks the one with the right name
                {
                    int unloadedProductsCount = storages.ElementAt<Storage>(i).UnloadVehicle(garageSlot);     // unloads products from vehicle and gets the total count
                    int productsInVehicle = 0;                                                                // <-- this will need to be checked
                    return $"Unloaded {unloadedProductsCount}/{productsInVehicle} products at {storageName}"; // returns the string with information
                }
            }
            return $"nothing unloaded at {storageName}"; // in case that there has been nothing unloaded
        }

        //public string GetStorageStatus(string storageName)
        //{
        //    string result = "";

        //    for (int i = 0; i < storages.Count; i++) // goes thru the list of all storages
        //    {
        //        if (storageName == storages.ElementAt<Storage>(i).Name) // checks the one with the right name
        //        {
        //            Storage storage = storages.ElementAt<Storage>(i); // gets a reference to the storage

        //            result = $"Stock ({storage.Weight}/{storage.Capacity}) ["; // gets the total weight and capacity and put it into result

        //            //This section is ment to get a list of products and count them
        //            List<Product> products = storage.Products().ToList<Product>();
        //            var prod =
        //                from product in products
        //                group product by product.GetType() into productType // MISSING ORDER BY Type
        //                select new
        //                {
        //                    type = productType.Key,
        //                    count = productType.Count()
        //                };

        //            int j;
        //            for (j = 0; j < prod.Count() - 1; j++)
        //            {
        //                result = result + prod.ElementAt(j).type + " " + prod.ElementAt(j).count + ", ";
        //            }
        //            result = result + prod.ElementAt(j).type + " " + prod.ElementAt(j).count + "]\n"; // the last element goes without a ,

        //            result = result + "Garage: [";
        //            List<Vehicle> garage = new List<Vehicle>(storage.Garage());
        //            for (j = 0; j < garage.Count; j++)
        //            {
        //                Vehicle vehicle = garage.ElementAt<Vehicle>(j);
        //                if (vehicle != null)
        //                    result = result + vehicle.GetType();
        //                else
        //                    result = result + "empty";
        //                if (j < garage.Count - 1) // the last element isn't followed by a pipe
        //                    result = result + "|";
        //            }
        //            result = result + "]";

        //            break; // comes out of the for, after processing the found storage
        //        }
        //    }
        //    return result;
        //}

        public string GetSummary()
        {
            string result = "";

            foreach (Storage storage in storages) // goes thru all storages
            {
                double totalMoney = 0;
                foreach (Product product in storage.Products())
                    totalMoney += product.Price;               // counts the total worth

                result = result + $"{storage.Name}:\nStorage worth: ${totalMoney:F2}\n";
            }

            return result;
        }

    }
}
