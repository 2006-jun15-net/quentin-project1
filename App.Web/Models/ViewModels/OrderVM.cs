using System.Collections.Generic;
using System.ComponentModel;
namespace App.Web.Models.ViewModels
{
    public class OrderVM
    {
        [DisplayName("Order Id")]
        public int Id { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItemVM> Items {get;set;}
    }
}
