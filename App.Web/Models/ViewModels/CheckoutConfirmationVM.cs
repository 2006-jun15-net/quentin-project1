using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DataAccess.Entities;
namespace App.Web.Models.ViewModels
{
    public class CheckoutConfirmationVM
    {
        public int OrderNumber { get; set; }
        public List<Order> FailedOrders { get; set; }
    }
}
