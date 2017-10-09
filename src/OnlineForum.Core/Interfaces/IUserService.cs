using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OnlineForum.Core.Models;

namespace OnlineForum.Core.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(int userId);
        CreateUserResponse CreateUser(string username, string password, string email);
        User SignIn(string username, string password);
        IEnumerable<Thread> GetUserThreads(int userId);
        IEnumerable<CommentNode> GetUserComments(int userId);
    }
}
