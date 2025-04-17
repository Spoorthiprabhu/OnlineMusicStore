using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMusicStore.Models
{
    public class MusicItem
    {
        public int MusicItemId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string SongURL { get; set; }
        public string ImagePath { get; set; }

        


        // 🔽 Add this
        public int CategoryId { get; set; }

        // 🔽 Navigation property
        [ForeignKey("CategoryId")]
        public virtual MusicCategory Category { get; set; }
    }
}
