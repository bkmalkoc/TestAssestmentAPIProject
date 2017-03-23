using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVCAPI.Models.Orders
{
    public class OrdersResponse
    {
        public string order_date { get; set; }
        public string order_number { get; set; }
        public List<OrderItem> order_items { get; set; }
    }
}
