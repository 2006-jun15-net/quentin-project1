using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class CheckoutVM
    {
        public int CustomerId { get; set; }
        public List<CheckoutItemVM> Items { get; set; }
    }
    public class CheckoutItemVM
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}