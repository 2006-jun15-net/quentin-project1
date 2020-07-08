using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models.ViewModels
{
    public class CustomerVM
    {
        [DisplayName("First Name")]
        public String FirstName { get; set; }
        [DisplayName("Last Name")]
        public String LastName { get; set; }
        [DisplayName("Location")]
        public string LocationName { get; set; }
        public int LocationId { get; set; }
        [DisplayName("Order History")]
        public List<int> OrderHistory { get; set; }
        public int Id { get; set; }
    }
}
