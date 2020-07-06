using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemViewModel> Items {get;set;}
    }
}
