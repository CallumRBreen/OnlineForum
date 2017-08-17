using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineForum.Core.Models
{
    public class CommentNode
    {
        public Comment Comment { get; set; }

        public IEnumerable<CommentNode> ChildComments { get; set; }
    }
}
