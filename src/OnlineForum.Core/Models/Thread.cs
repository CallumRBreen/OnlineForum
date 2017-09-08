using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OnlineForum.Core.Models
{
    public class Thread
    {
        public int ThreadId { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter content.")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Modified { get; set; }

        public List<ThreadVote> Votes { get; set; }

        public int GetScore() => Votes.Sum(x => x.VoteScore);

        public User User { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
