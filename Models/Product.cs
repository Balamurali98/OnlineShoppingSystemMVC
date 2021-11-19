using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class Product
    {
        public Product()
        {
           this.Orders = new HashSet<Order>();
        }


        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string RetailerId { get; set; }
        public string Description { get; set; }

        [DisplayName(" Product Image")]
        public string ProductImage { get; set; }
        public string Features { get; set; }
        public int? AvailableProduct { get; set; }
        public decimal? Price { get; set; }
        [NotMapped]
        [DisplayName("Product Image")]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public int quantity { get; set; }

        [NotMapped]
        public int TotalPrice { get; set; }


        public virtual Category Category { get; set; }
        public virtual Retailer Retailer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
