using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OnlineForum.DAL;

namespace OnlineForum.Dal
{
    public class OnlineForumContextFactory : IDbContextFactory<OnlineForumContext>
    {
        public OnlineForumContext Create(DbContextFactoryOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OnlineForumContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlineForumDatabase;Trusted_Connection=True;MultipleActiveResultSets=true", b=>b.MigrationsAssembly("OnlineForum.DAL"));

            return new OnlineForumContext(optionsBuilder.Options);
        }
    }
}
