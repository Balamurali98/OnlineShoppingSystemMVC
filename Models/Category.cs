using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Descriptions { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
