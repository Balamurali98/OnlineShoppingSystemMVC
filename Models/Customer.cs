using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Please enter customername")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter customerAddress")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string Phonenumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Confirmpassword { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
