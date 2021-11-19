using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class OrderDetail
    {
        private int qn;
        public OrderDetail()
        { }
        public OrderDetail(Product product,int qn)
        {
            this.Product = product;
            this.qn = qn;
        }
        [ Key]
        public int? OrderId { get; set; }

        [Key]
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        [NotMapped]
        public decimal? TotalPrice { get; set; }
        public decimal? Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
