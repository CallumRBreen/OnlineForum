using System;
using System.Collections.Generic;
using System.Text;
using OnlineForum.Core.Models;

namespace OnlineForum.Core.Interfaces
{
    public interface ICommentService
    {
        void CreateComment(Comment comment);

    }
}
