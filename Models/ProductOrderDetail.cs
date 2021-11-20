using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class ProductOrderDetail
    {
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? BillId { get; set; }
        public string Customername { get; set; }
        public DateTime? OrderedDate { get; set; }
        public string Address { get; set; }
        public string Productname { get; set; }

        public virtual BankDetail Bill { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProductOrder Order { get; set; }
    }
}
