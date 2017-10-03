using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineForum.DAL.Entities
{
    public class CommentVote
    {
        public int CommentVoteId { get; set; }
        public int VoteScore { get; set; }

        public User VoteBy { get; set; }
    }
}
