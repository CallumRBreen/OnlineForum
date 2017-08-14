using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineForum.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OnlineForum.DAL
{
    public class OnlineForumContext : DbContext
    {
        public OnlineForumContext(DbContextOptions<OnlineForumContext> options) : base(options)
        {
            
        }

        public DbSet<Thread> Threads { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
