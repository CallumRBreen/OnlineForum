using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineForum.DAL;

namespace OnlineForum.Core.Tests
{
    public class ServiceTestBase
    {
        protected static OnlineForumContext GetNewContext()
        {
            var options = GetOptions();
            
            var context = new OnlineForumContext(options);

            return context;
        }

        protected static IMapper GetMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfiles("OnlineForum.Core"));

            return config.CreateMapper();
        }

        protected static DbContextOptions<OnlineForumContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<OnlineForumContext>()
                .UseInMemoryDatabase(databaseName: "ServiceTestsDatabase")
                .Options;

            return options;
        }
    }
}
