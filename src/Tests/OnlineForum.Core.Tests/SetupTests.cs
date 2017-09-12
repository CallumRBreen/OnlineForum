using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineForum.DAL;

namespace OnlineForum.Core.Tests
{
    [SetUpFixture]
    public class SetupTests
    {
        [OneTimeSetUp]
        public void InitialiseData()
        {
            var options = new DbContextOptionsBuilder<OnlineForumContext>()
                .UseInMemoryDatabase(databaseName: "ServiceTestsDatabase")
                .Options;

            var context = new OnlineForumContext(options);

            using (context)
            {
                DbInitializer.InitializeTestData(context);
            }

        }
    }
}
