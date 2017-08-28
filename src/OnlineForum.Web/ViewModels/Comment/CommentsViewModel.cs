using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineForum.Core.Models;

namespace OnlineForum.Web.ViewModels.Comment
{
    public class CommentsViewModel
    {
        public Thread Thread { get; set; }
        public IEnumerable<OnlineForum.Core.Models.CommentNode> Comments { get; set; }
    }
}
