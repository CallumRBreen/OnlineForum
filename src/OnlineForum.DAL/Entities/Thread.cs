﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineForum.DAL.Entities
{
    public class Thread
    {
        public int ThreadId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public List<ThreadVote> Votes { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
