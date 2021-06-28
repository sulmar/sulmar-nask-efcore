using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulmar.EFCore.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }   // orders.Where(o=>o.Customer.Id == 100)  // orders.Where(o=>o.CustomerId == 100)

        // public int CustomerId { get; set; } // shadow property
        public IEnumerable<OrderDetail> Details { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount => Details.Sum(d => d.LineAmount);
    }

    public enum OrderStatus
    {
        Ordered,
        Sent,
        Canceled,
        Shipped
    }
}





