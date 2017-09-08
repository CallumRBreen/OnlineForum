using System;
using System.Collections.Generic;
using System.Text;
using OnlineForum.Core.Models;

namespace OnlineForum.Core.Interfaces
{
    public interface IThreadService
    {
        IEnumerable<Thread> GetThreads();
        Thread GetThread(int threadId);
        int CreateThread(Thread thread);
        void EditThread(Thread thread);
        void DeleteThread(int threadId);
        VoteResult Upvote(int threadId, int userId);
        VoteResult Downvote(int threadId, int userId);
    }
}
