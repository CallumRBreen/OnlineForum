using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.DAL;
using BCrypt.Net;


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

        public int CreateUser(string username, string password, string email) // Add enum, with response type e.g. successful, user already exists
        {
            var user = new DAL.Entities.User
            {
                UserName = username,
                PasswordHash = GetPasswordHash(password),
                Email = email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId;
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
    }
}
