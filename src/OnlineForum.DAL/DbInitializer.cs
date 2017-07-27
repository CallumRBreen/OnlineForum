using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineForum.DAL.Entities;

namespace OnlineForum.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(OnlineForumContext context)
        {
            context.Database.EnsureCreated();

            if (context.Threads.Any())
            {
                return;
            }

            var threads = new List<Thread>()
            {
                new Thread()
                {
                    Title = "This is title 1",
                    Content = "This is the content, this is the content",
                    Created = DateTime.Now,
                    Modified = DateTime.Now

                },
                new Thread()
                {
                    Title = "This is title 55",
                    Content = "This is the content, this is the content",
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                },
                new Thread()
                {
                    Title = "This is title 2",
                    Content = "This is the content, this is the content",
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                },
                new Thread()
                {
                    Title = "This is title 3",
                    Content = "This is the content, this is the content",
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                },
                new Thread()
                {
                    Title = "This is title 4",
                    Content = "This is the content, this is the content",
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                }
            };

            context.Threads.AddRange(threads);
            context.SaveChanges();

        }
    }
    }

