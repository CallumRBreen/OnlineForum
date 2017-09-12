using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.DAL;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;


namespace OnlineForum.Core.Implementations
{
    public class UserService : IUserService
    {
        private readonly OnlineForumContext _context;
        private readonly IMapper _mapper;

        public UserService(OnlineForumContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _context.Users.ToList();

            return _mapper.Map<IEnumerable<User>>(users);
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user == null) return null;

            return _mapper.Map<User>(user);
        }

        public CreateUserResponse CreateUser(string username, string password, string email)
        {
            var user = new DAL.Entities.User
            {
                UserName = username,
                PasswordHash = GetPasswordHash(password),
                Email = email
            };

            var createUserResponse = new CreateUserResponse();

            if (_context.Users.Any(u => u.UserName == username))
            {
                createUserResponse.Success = false;
                createUserResponse.Message = "Username already taken.";
                return createUserResponse;
            }

            if (_context.Users.Any(u => u.Email == email))
            {
                createUserResponse.Success = false;
                createUserResponse.Message = "Email already taken.";
                return createUserResponse;
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            createUserResponse.Success = true;
            createUserResponse.Message = $"Successfully created user {user.UserName}.";

            return createUserResponse;
        }

        public User SignIn(string username, string password)
        {
            var foundUser = _context.Users.FirstOrDefault(x => x.UserName.Equals(username) 
                                           && BCrypt.Net.BCrypt.Verify(password, x.PasswordHash));

            return _mapper.Map<User>(foundUser);
        }

        private string GetPasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public IEnumerable<Thread> GetUserThreads(int userId)
        {
            var threads = _context.Threads.Include(u => u.User).Where(x => x.User.UserId == userId);

            return _mapper.Map<IEnumerable<Thread>>(threads);
        }

        public IEnumerable<Comment> GetUserComments(int userId)
        {
            var threads = _context.Comments.Include(u => u.User).Where(x => x.User.UserId == userId);

            return _mapper.Map<IEnumerable<Comment>>(threads);
        }
    }
}
