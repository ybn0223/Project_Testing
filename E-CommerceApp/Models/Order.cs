using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Models
{
    public class Order
    {
        public List<OrderItem>? Items { get; set; }
        public double Total { get; set; }
        public bool DiscountApplied { get; set; }
    }
}
