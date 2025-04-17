using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineMusicStore.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int MusicItemId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("MusicItemId")]
        public virtual MusicItem MusicItem { get; set; }
    }
}