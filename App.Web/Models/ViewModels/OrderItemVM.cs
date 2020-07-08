using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class OrderItemVM
    {
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public string Location { get; set; }
    }
}
