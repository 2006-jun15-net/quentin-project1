using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Models.ViewModels
{
    public class CustomerVM
    {
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^[a-z ,.'-]+$", ErrorMessage = "A First Name cannot contain \\ / : * ? \" < > | or numbers")]
        [Required(ErrorMessage = "A First Name Is Required")]
        [DisplayName("First Name")]
        public String FirstName { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[a-z ,.'-]+$", ErrorMessage = "A Last Name cannot contain \\ / : * ? \" < > | or numbers")]
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "A Last Name Is Required")]
        public String LastName { get; set; }
        [DisplayName("Location")]
        public string LocationName { get; set; }
        public int LocationId { get; set; }
        [DisplayName("Order History")]
        public List<int> OrderHistory { get; set; }
        public int Id { get; set; }
    }
}
