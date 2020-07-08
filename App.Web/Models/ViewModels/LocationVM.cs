using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class LocationVM
    {
        public string Name { get; set; }
        public List<InventoryVM> Inventory { get; set; }
        public List<OrderVM> Orders { get; set; }

    }
}
