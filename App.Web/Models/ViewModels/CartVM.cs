using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class CartVM
    {
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int Qty { get; set; }
        public string ProductName { get; set; }
        public string LocationName { get; set; }
    }
}
