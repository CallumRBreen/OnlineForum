using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineForum.Core.Models
{
    public class Thread
    {
        public int ThreadId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public int GetScore() => (Upvotes + Downvotes);
    }
}
