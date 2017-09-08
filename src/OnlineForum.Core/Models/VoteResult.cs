using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineForum.Core.Models
{
    public class VoteResult
    {
        public bool DidScoreIncrease { get; set; }
        public int Score { get; set; }
    }
}
