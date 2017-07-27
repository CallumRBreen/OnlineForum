using System;
using System.Collections.Generic;
using System.Text;
using OnlineForum.Core.Models;

namespace OnlineForum.Core.Interfaces
{
    interface IThreadService
    {
        IEnumerable<Thread> GetThreads();
        Thread GetThread(int threadId);
        void CreateThread(Thread thread);
        void EditThread(Thread thread);
        void DeleteThread(int threadId);
        void Upvote(int threadId);
        void Downvote(int threadId);
    }
}
