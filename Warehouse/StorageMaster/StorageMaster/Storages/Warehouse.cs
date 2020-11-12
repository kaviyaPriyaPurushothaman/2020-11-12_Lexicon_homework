using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Models.Storages
{
    class Warehouse : Storage
    {
        // creates a Warehouse with 3 semi trucks
        public Warehouse(string name) : base(name, 10, 10, new[] { new Semi(), new Semi(), new Semi() } )
        {

        }
    }
}
