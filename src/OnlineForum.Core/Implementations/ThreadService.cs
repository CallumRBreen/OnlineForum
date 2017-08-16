using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.DAL;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace OnlineForum.Core.Implementations
{
    public class ThreadService : IThreadService
    {
        private readonly OnlineForumContext _context;
        private readonly IMapper _mapper;

        public ThreadService(OnlineForumContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Thread> GetThreads()
        {
            var entityThreads = _context.Threads.Include(t => t.User).ToList(); // Can change this once EF includes lazy loading

            return _mapper.Map<IEnumerable<Thread>>(entityThreads);
        }

        public Thread GetThread(int threadId)
        {
            var entityThread = _context.Threads.Include(t => t.User).FirstOrDefault(x => x.ThreadId == threadId);

            if (entityThread == null) return null;

            return _mapper.Map<Thread>(entityThread);
        }

        public int CreateThread(Thread thread)
        {
            var entityThread = _mapper.Map<DAL.Entities.Thread>(thread);

            entityThread.Created = DateTime.Now;
            entityThread.Modified = DateTime.Now;
            entityThread.Downvotes = 0;
            entityThread.Upvotes = 0;

            _context.Threads.Add(entityThread);
            _context.SaveChanges();

            return entityThread.ThreadId;
        }

        public void EditThread(Thread thread)
        {
            var entityThread = _context.Threads.Find(thread.ThreadId);

            if (entityThread == null) return;

            entityThread.Title = thread.Title;
            entityThread.Content = thread.Content;
            entityThread.Modified = DateTime.Now;

            _context.Threads.Update(entityThread);
            _context.SaveChanges();
        }

        public void DeleteThread(int threadId)
        {
            var threadToDelete = _context.Threads.Find(threadId);

            if (threadToDelete == null) return;

            _context.Threads.Remove(threadToDelete);
            _context.SaveChanges();
        }

        public void Upvote(int threadId)
        {
            var entityThread = _context.Threads.Find(threadId);

            if (entityThread == null) return;

            entityThread.Upvotes++;
            _context.SaveChanges();
        }

        public void Downvote(int threadId)
        {
            var entityThread = _context.Threads.Find(threadId);

            if (entityThread == null) return;

            entityThread.Downvotes++;
            _context.SaveChanges();
        }
    }
}
