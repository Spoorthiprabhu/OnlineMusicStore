using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusicStore.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; }

        public virtual User User { get; set; }
    }
}