using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class CartVM
    {
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int Qty { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Total => this.Qty * this.Price;
        public string ProductName { get; set; }
        public string LocationName { get; set; }
        public int Stock { get; set; }
    }
}
