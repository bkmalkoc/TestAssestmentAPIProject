using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVCAPI.Models
{
    public class DistrubitionMap
    {
        public List<string> OrderName { get; set; }
        public List<string> DistributionPrice { get; set; }
        public string TotalAmount { get; set; }
    }
}
