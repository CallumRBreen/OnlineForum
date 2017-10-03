﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using OnlineForum.DAL.Entities;

namespace OnlineForum.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(OnlineForumContext context)
        {
        }

        public static void InitializeTestData(OnlineForumContext context)
        {
            if (context.Threads.Any())
            {
                return;
            }

            var user = new User
            {
                UserName = "TestUser",
                PasswordHash = "$2b$10$qk5Kn8tGXmVVoJYRw4S7UuttlPlNH6sQWEDCCieObLamPVFR94lAW",
                Email = "TestEmail@Test.com"
            };

            context.Users.Add(user);
            context.SaveChanges();

            var threads = new List<Thread>();

            var random = new Random();

            for (var i = 1; i <= 25; i++)
            {
                var thread = new Thread
                {
                    Title = "Title" + i,
                    Content = "Content" + i,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    User = user,
                    Votes = new List<ThreadVote>()
                    {
                        new ThreadVote()
                        {
                            VoteBy = user,
                            VoteScore = 1,
                        }
                    }
                };

                threads.Add(thread);

            }
            
            context.Threads.AddRange(threads);

            context.SaveChanges();

            var comment = new Comment
            {
                Thread = threads.First(),
                User = user,
                Content = "Top Comment",
                Modified = DateTime.Now,
                Created = DateTime.Now,
                Parent = null,
                Votes = new List<CommentVote>()
                {
                    new CommentVote()
                    {
                        VoteScore = 1,
                        VoteBy = user
                    }
                }
            };

            context.Comments.Add(comment);

            var commentTwo = new Comment
            {
                Thread = threads.First(),
                User = user,
                Content = "Top Comment Reply 1",
                Modified = DateTime.Now,
                Created = DateTime.Now,
                Parent = comment,
                Votes = new List<CommentVote>()
                {
                    new CommentVote()
                    {
                        VoteScore = 1,
                        VoteBy = user
                    }
                }
            };

            context.Comments.Add(commentTwo);

            var commentThree = new Comment
            {
                Thread = threads.First(),
                User = user,
                Content = "Top Comment Reply 2",
                Modified = DateTime.Now,
                Created = DateTime.Now,
                Parent = comment,
                Votes = new List<CommentVote>()
                {
                    new CommentVote()
                    {
                        VoteScore = 1,
                        VoteBy = user
                    }
                }
            };

            context.Comments.Add(commentThree);

            var commentFour = new Comment
            {
                Thread = threads.First(),
                User = user,
                Content = "Top Comment Reply 2 Reply 1",
                Modified = DateTime.Now,
                Created = DateTime.Now,
                Parent = commentThree,
                Votes = new List<CommentVote>()
                {
                    new CommentVote()
                    {
                        VoteScore = 1,
                        VoteBy = user
                    }
                }
            };

            context.Comments.Add(commentFour);

            context.SaveChanges();

        }
    }
}

