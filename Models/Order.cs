using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusicStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        // Optional: Link to User model (if exists)
        public virtual User User { get; set; }
    }
}