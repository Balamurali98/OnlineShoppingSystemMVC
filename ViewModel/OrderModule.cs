using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingSystem.ViewModel
{
    public class OrderModule
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
