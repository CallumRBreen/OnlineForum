using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineForum.Core.Implementations;
using OnlineForum.DAL;
using OnlineForum.DAL.Entities;
using ThreadVote = OnlineForum.Core.Models.ThreadVote;

namespace OnlineForum.Core.Tests
{
    [TestFixture]
    class ThreadServiceTests : TestBase
    {
        [Test]
        public void ThreadCreateSuccess()
        {
            using (var context = GetNewContext())
            {
                var threadService = GetThreadService(context);

                var threadId = threadService.CreateThread(new Models.Thread()
                {
                    Title = "New Title",
                    Content = "New Content",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Votes = new List<ThreadVote>()
                    {
                        new ThreadVote()
                        {
                            VoteScore = 1
                        }
                    } 
                });

                Assert.Greater(threadId, 0);
            }
        }

        [Test]
        public void ThreadDeleteSuccess()
        {
            using (var context = GetNewContext())
            {
                var threadService = GetThreadService(context);

                threadService.DeleteThread(10);

                Assert.IsNull(threadService.GetThread(10));
            }
        }

        [Test]
        public void ThreadEditSuccess()
        {
            Models.Thread uneditedThread;

            using (var context = GetNewContext())
            {
                var threadService = GetThreadService(context);

                uneditedThread = threadService.GetThread(20);
            }

            using (var context = GetNewContext())
            {
                var threadService = GetThreadService(context);

                var thread = new Models.Thread()
                {
                    ThreadId = 20,
                    Title = "New Title",
                    Content = "New Content",
                    //Upvotes = 666,
                    //Downvotes = 666,
                    Modified = DateTime.Now,
                    Created = DateTime.Now
                };

                threadService.EditThread(thread);
                var editedThread = threadService.GetThread(thread.ThreadId);

                Assert.AreEqual(thread.ThreadId, editedThread.ThreadId);
                Assert.AreEqual(thread.Title, editedThread.Title);
                Assert.AreEqual(thread.Content, editedThread.Content);
                Assert.AreNotEqual(uneditedThread.Title, editedThread.Title);
                Assert.AreNotEqual(uneditedThread.Content, editedThread.Content);   
            }
        }

        private static ThreadService GetThreadService(OnlineForumContext context)
        {
            var mapper = GetMapper();

            var threadService = new ThreadService(context, mapper);

            return threadService;
        }
    }
}
