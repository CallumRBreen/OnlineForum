using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.DAL;

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
            var entityThreads = _context.Threads.ToList();

            return _mapper.Map<IEnumerable<Thread>>(entityThreads);
        }

        public Thread GetThread(int threadId)
        {
            var entityThread = _context.Threads.FirstOrDefault(x => x.ThreadId == threadId);

            return _mapper.Map<Thread>(entityThread);
        }

        public void CreateThread(Thread thread)
        {
            var entityThread = _mapper.Map<DAL.Entities.Thread>(thread);

            _context.Threads.Add(entityThread);
            _context.SaveChanges();
        }

        public void EditThread(Thread thread)
        {
            var entityThread = _mapper.Map<DAL.Entities.Thread>(thread);

            _context.Threads.Update(entityThread);
            _context.SaveChanges();
        }

        public void DeleteThread(int threadId)
        {
            var threadToDelete = _context.Threads.Find(threadId);
            _context.Threads.Remove(threadToDelete);
            _context.SaveChanges();
        }

        public void Upvote(int threadId)
        {
            _context.Threads.Find(threadId).Upvotes++;
            _context.SaveChanges();
        }

        public void Downvote(int threadId)
        {
            _context.Threads.Find(threadId).Downvotes--;
            _context.SaveChanges();
        }
    }
}
