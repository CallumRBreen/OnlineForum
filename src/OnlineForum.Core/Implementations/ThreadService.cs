﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using OnlineForum.Core.Interfaces;
using OnlineForum.DAL;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OnlineForum.Core.Models;
using Thread = OnlineForum.Core.Models.Thread;
using ThreadVote = OnlineForum.DAL.Entities.ThreadVote;


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
            var entityThreads = _context.Threads.Include(u => u.User)
                                                .Include(v => v.Votes)
                                                .Include(c => c.Comments);

            return _mapper.Map<IEnumerable<Thread>>(entityThreads);
        }

        public Thread GetThread(int threadId)
        {
            var entityThread = _context.Threads.Include(u => u.User)
                                               .Include(v => v.Votes)
                                               .Include(c => c.Comments)
                                               .FirstOrDefault(x => x.ThreadId == threadId);

            if (entityThread == null) return null;

            return _mapper.Map<Thread>(entityThread);
        }

        public int CreateThread(Thread thread)
        {
            var entityThread = _mapper.Map<DAL.Entities.Thread>(thread);

            entityThread.Created = DateTime.Now;
            entityThread.Modified = DateTime.Now;

            var threadVote = new ThreadVote()
            {
                VoteBy = entityThread.User,
                VoteScore = 1
            };

            entityThread.Votes.Add(threadVote);

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
            var threadToDelete = _context.Threads.Include(c => c.Comments)
                                                 .FirstOrDefault(x => x.ThreadId == threadId);

            if (threadToDelete == null) return;

            threadToDelete.Comments.ForEach(x => _context.Remove(x));
            _context.Threads.Remove(threadToDelete);
            _context.SaveChanges();
        }

        public VoteResult Upvote(int threadId, int userId)
        {
            var thread = _context.Threads.Include(u => u.User)
                                         .Include(v => v.Votes)
                                         .Include(c => c.Comments)
                                         .First(x => x.ThreadId == threadId);

            var user = _context.Users.First(x => x.UserId == userId);

            var vote = thread.Votes.FirstOrDefault(x => x.VoteBy.UserId == user.UserId);

            if (vote == null)
            {
                vote = new ThreadVote()
                {
                    VoteScore = 1,
                    VoteBy = user

                };

                thread.Votes.Add(vote);
            }
            else
            {
                vote.VoteScore = vote.VoteScore == 1 ? 0 : 1;
            }

            _context.SaveChanges();

            return new VoteResult()
            {
                Score = thread.Votes.Sum(x => x.VoteScore),
                DidScoreIncrease = vote.VoteScore == 1,
            };
        }

        public VoteResult Downvote(int threadId, int userId)
        {
            var thread = _context.Threads.Include(u => u.User)
                                         .Include(v => v.Votes)
                                         .Include(c => c.Comments)
                                         .First(x => x.ThreadId == threadId);

            var user = _context.Users.First(x => x.UserId == userId);

            var vote = thread.Votes.FirstOrDefault(x => x.VoteBy.UserId == user.UserId);

            if (vote == null)
            {
                vote = new ThreadVote()
                {
                    VoteScore = -1,
                    VoteBy = user

                };

                thread.Votes.Add(vote);
            }
            else
            {

                vote.VoteScore = vote.VoteScore == -1 ? 0 : -1;
            }

            _context.SaveChanges();

            return new VoteResult()
            {
                Score = thread.Votes.Sum(x => x.VoteScore),
                DidScoreIncrease = vote.VoteScore == 0,
            };
        }
    }
}
