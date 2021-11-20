using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class ProductOrder
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public string Productname { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public DateTime? OrderedDate { get; set; }
        public int? CustomerId { get; set; }
        [NotMapped]
        public decimal? unitprice { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
