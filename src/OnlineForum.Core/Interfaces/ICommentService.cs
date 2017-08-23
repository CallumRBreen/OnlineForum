using System;
using System.Collections.Generic;
using System.Text;
using OnlineForum.Core.Models;

namespace OnlineForum.Core.Interfaces
{
    public interface ICommentService
    {
        void CreateComment(string content, Comment parentComment, User user, Thread thread);

        IEnumerable<CommentNode> GetComments(int threadId);

        Comment GetComment(int commentId);

    }
}
