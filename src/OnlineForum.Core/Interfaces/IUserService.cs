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
        int CreateUser(string username, string password, string email);
        User SignIn(string username, string password);
    }
}
