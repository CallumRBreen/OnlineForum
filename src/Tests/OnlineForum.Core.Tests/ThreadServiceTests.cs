using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineForum.Core.Implementations;
using OnlineForum.DAL;
using OnlineForum.DAL.Entities;

namespace OnlineForum.Core.Tests
{
    [TestFixture]
    class ThreadServiceTests
    {
        [Test]
        public void ThreadCreateSuccessful()
        {
            IntialiseTestData();

            using (var context = GetNewContext())
            {
                var threadService = GetThreadService(context);

                var threadId = threadService.CreateThread(new Models.Thread()
                {
                    Title = "New Title",
                    Content = "New Content",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Upvotes = 99,
                    Downvotes = 435
                });

                Assert.Greater(threadId, 0);
            }
        }

        [Test]
        public void ThreadDeleteSuccessful()
        {
            IntialiseTestData();

            using (var context = GetNewContext())
            {
                var threadService = GetThreadService(context);

                threadService.DeleteThread(10);

                Assert.IsNull(threadService.GetThread(10));
            }
        }

        [Test]
        public void ThreadEditSuccessful()
        {
            IntialiseTestData();

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
                    Upvotes = 666,
                    Downvotes = 666,
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

        private static void IntialiseTestData()
        {
            using (var context = GetNewContext())
            {
                DbInitializer.Initialize(context);
            }
        }

        private static OnlineForumContext GetNewContext()
        {
            var options = GetOptions();
            
            var context = new OnlineForumContext(options);

            return context;
        }

        public static ThreadService GetThreadService(OnlineForumContext context)
        {
            var mapper = GetMapper();

            var threadService = new ThreadService(context, mapper);

            return threadService;
        }

        private static IMapper GetMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg => { cfg.AddProfiles("OnlineForum.Core"); });

            return config.CreateMapper();
        }

        private static DbContextOptions<OnlineForumContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<OnlineForumContext>()
                .UseInMemoryDatabase(databaseName: "ThreadServiceTestsDatabase")
                .Options;

            return options;
        }
    }
}
