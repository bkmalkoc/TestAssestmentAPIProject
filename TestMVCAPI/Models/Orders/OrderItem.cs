using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVCAPI.Models.Orders
{
    public class OrderItem
    {
        public int order_item_id { get; set; }
        public string type { get; set; }
        public int pages { get; set; }
    }
}
