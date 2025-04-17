using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusicStore.Models
{
    public class WishList
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int MusicItemId { get; set; }

        public virtual User User { get; set; }
        public virtual MusicItem MusicItem { get; set; }
    }
}