using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVCAPI.Models.Fees
{
    public class FeesResponse
    {
        public string order_item_type { get; set; }
        public List<Fee> fees { get; set; }
        public List<Distribution> distributions { get; set; }
    }
}
