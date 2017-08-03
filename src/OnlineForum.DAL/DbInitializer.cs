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

            var threads = new List<Thread>();

            for (var i = 1; i <= 50; i++)
            {
                var thread = new Thread
                {
                    Title = "Title" + i,
                    Content = "Content" + i,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Upvotes = new Random().Next(1, 100),
                    Downvotes = new Random().Next(1, 100)
                };

                threads.Add(thread);

            }
            
            context.Threads.AddRange(threads);
            context.SaveChanges();

        }
    }
}

