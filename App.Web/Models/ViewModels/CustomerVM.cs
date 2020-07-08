using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class CustomerVM
    {
        [DisplayName("Name")]
        public String Name { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; }
        [DisplayName("Order History")]
        public List<int> OrderHistory { get; set; }
        public int Id { get; set; }
    }
}
