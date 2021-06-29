using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulmar.EFCore.Models
{
    public class Order : BaseEntity
    {
        private readonly ILazyLoader lazyLoader;

        public DateTime OrderDate { get; set; }


        // public virtual Customer Customer { get; set; }  // Navigation property

        private Customer customer;
        public Customer Customer
        {
            get => lazyLoader.Load(this, ref customer);

            set { customer = value; }
        }


        // orders.Where(o=>o.Customer.Id == 100)  // orders.Where(o=>o.CustomerId == 100)

        // public int CustomerId { get; set; } // shadow property
        // public virtual ICollection<OrderDetail> Details { get; set; }


        private ICollection<OrderDetail> details;
        public ICollection<OrderDetail> Details
        {
            get => lazyLoader.Load(this, ref details);
            set => details = value;
        }

        public OrderStatus Status { get; set; }
        public decimal TotalAmount => Details.Sum(d => d.LineAmount);

        public Order()
        {

        }

        public Order(ILazyLoader lazyLoader)
        {
            Details = new HashSet<OrderDetail>();

            OrderDate = DateTime.Now;

            CreatedOn = DateTime.Now;

            this.lazyLoader = lazyLoader;
        }
    }

    public enum OrderStatus
    {
        Ordered,
        Sent,
        Canceled,
        Shipped
    }
}





