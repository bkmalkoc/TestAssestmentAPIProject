using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVCAPI.Models
{
    public class OrderMap
    {
        public List<string> OrderName { get; set; }
        public List<string> OrderPrice { get; set; }
        public string TotalAmount { get; set; }
    }
}
