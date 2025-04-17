using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusicStore.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int MusicItemId { get; set; }
        public int? UserId { get; set; } // Optional

        public DateTime VotedAt { get; set; }

        public virtual MusicItem MusicItem { get; set; }
        public virtual User User { get; set; }
    }

}