using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineMusicStore.Models
{
    public class MusicStoreContext : DbContext
    {
        public MusicStoreContext() : base("name=MusicStoreContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<MusicItem> MusicItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<MusicCategory> MusicCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Vote> Votes { get; set; }



    }
}