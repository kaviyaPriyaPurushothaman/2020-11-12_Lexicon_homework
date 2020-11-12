using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Models.Storages
{
    class AutomatedWarehouse:Storage
    {
        // Creates an Automated Warehouse with 1 Truck
        public AutomatedWarehouse(string name) : base(name, 1, 2, new[] { new Truck() } )
        {

        }
    }
}
