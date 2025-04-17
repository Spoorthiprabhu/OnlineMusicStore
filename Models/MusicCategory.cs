using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineMusicStore.Models
{
    public class MusicCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        // Navigation property
        public virtual ICollection<MusicItem> MusicItems { get; set; }


    }
}