using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public string Productname { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? ExpectedDelivery { get; set; }
        public string Paymentmode { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
