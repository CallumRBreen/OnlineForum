using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.DAL;

namespace OnlineForum.Core.Implementations
{
    public class ThreadService : IThreadService
    {
        private OnlineForumContext _context;

        public ThreadService(OnlineForumContext context)
        {
            _context = context;
        }

        public IEnumerable<Thread> GetThreads()
        {
            throw new NotImplementedException();
        }

        public Thread GetThread(int threadId)
        {
            throw new NotImplementedException();
        }

        public void CreateThread(Thread thread)
        {
            throw new NotImplementedException();
        }

        public void EditThread(Thread thread)
        {
            throw new NotImplementedException();
        }

        public void DeleteThread(int threadId)
        {
            var threadToDelete = _context.Threads.FirstOrDefault(x => x.ThreadId == threadId);
            _context.Threads.Remove(threadToDelete);
            _context.SaveChanges();
        }

        public void Upvote(int threadId)
        {
            _context.Threads.FirstOrDefault(x => x.ThreadId == threadId).Upvotes++;
            _context.SaveChanges();
        }

        public void Downvote(int threadId)
        {
            _context.Threads.FirstOrDefault(x => x.ThreadId == threadId).Upvotes--;
            _context.SaveChanges();
        }
    }
}
