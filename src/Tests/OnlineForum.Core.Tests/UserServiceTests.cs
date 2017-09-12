using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OnlineForum.Core.Implementations;
using OnlineForum.DAL;

namespace OnlineForum.Core.Tests
{
    [TestFixture]
    public class UserServiceTests : ServiceTestBase
    {
        [Test]
        public void CreateUserSuccess()
        {
            using (var context = GetNewContext())
            {
                var userService = GetUserService(context);

                var createUserResponse = userService.CreateUser("callumtest", "password123", "callumtest@asdas.com");
                Assert.IsTrue(createUserResponse.Success);
            }
        }

        [Test]
        public void SignInSuccess()
        {
            using (var context = GetNewContext())
            {
                var userService = GetUserService(context);

                var result = userService.SignIn("TestUser", "TestPassword123");

                Assert.IsNotNull(result);
            }
        }

        [Test]
        public void SignInFailure()
        {
            using (var context = GetNewContext())
            {
                var userService = GetUserService(context);

                var result = userService.SignIn("asdasdasdas", "TestPadasdasdasdassssword123");

                Assert.IsNull(result);
            }
        }

        private static UserService GetUserService(OnlineForumContext context)
        {
            var mapper = GetMapper();

            var userService = new UserService(context, mapper);

            return userService;
        }
    }
}
