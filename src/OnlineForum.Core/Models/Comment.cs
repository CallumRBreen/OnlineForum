using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineForum.Core.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public User User { get; set; }

        public Thread Thread { get; set; }

        public Comment Parent { get; set; }

        public IEnumerable<CommentVote> Votes { get; set; }

        public int GetScore() => Votes.Sum(v => v.VoteScore);
    }
}
