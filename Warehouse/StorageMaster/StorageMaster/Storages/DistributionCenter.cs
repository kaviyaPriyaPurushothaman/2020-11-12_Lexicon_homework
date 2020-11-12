using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Models.Storages
{
    class DistributionCenter : Storage
    {
        // Creates a distribution center with 3 vans
        public DistributionCenter(string name) : base(name, 2, 5, new[] { new Van(), new Van(), new Van() })
        {

        }
    }
}
