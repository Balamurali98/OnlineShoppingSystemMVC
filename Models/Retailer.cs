using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class Retailer
    {
       
            public Retailer()
            {
                Products = new HashSet<Product>();
            }


            [Required(ErrorMessage = "Please Enter Retailer ID")]
            public string RetailerId { get; set; }
            [Required(ErrorMessage = "Please Enter RetailerName")]
            public string RetailerName { get; set; }
            public string Gender { get; set; }
            public string Address { get; set; }
            [Required(ErrorMessage = "Please Enter PhoneNumber")]
            [DataType(DataType.PhoneNumber)]
            public string Phonenumber { get; set; }
            [Required(ErrorMessage = "Please Enter Mail Id")]
            [DataType(DataType.EmailAddress)]
            public string EmailId { get; set; }
            [Required(ErrorMessage = "Please Enter Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Display(Name = "Confirm password")]
            [Required(ErrorMessage = "Please enter confirm password")]
            [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
            [DataType(DataType.Password)]
            [NotMapped]
            public string Confirmpassword { get; set; }


            public virtual ICollection<Product> Products { get; set; }
        }
    }

