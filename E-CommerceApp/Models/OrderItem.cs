using E_CommerceApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Models
{
    public class OrderItem : IOrderItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
