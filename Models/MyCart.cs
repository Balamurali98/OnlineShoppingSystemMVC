using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingSystem.Models
{
    public class MyCart
    {
        public MyCart()
        {
            Products = new HashSet<Product>();
        }
        public int proid { get; set; }

        public string proname { get; set; }

        public int qty { get; set; }

        public int price { get; set; }

        public int bill { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
