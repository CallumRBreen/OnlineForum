using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineForum.DAL.Entities
{
    public class ThreadVote
    {
        public int ThreadVoteId { get; set; }
        public int VoteScore { get; set; }

        public Thread Thread { get; set; }
        public User VoteBy { get; set; }
    }
}
