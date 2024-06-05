using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Entities.Order_Aggregate
{
    public class Address
    {
        /*the Address is a composite attribute not a table [each order has address as a composite attribute]
        *or total participation one to one between the order and the address*/

       // [Required]//this attribute for database mapping and for displaying error message for user if it was null
        public required string  FirstName { get; set; }//the required keyword is a C# keyword that doesn't allow the null [throw exception in case of null]
        public string LastName { get; set; } = null!;
        public string Street { get; set; }= null!;
        public string City { get; set; } = null!;
        public string Country { get; set; }=null!;
    }
}
