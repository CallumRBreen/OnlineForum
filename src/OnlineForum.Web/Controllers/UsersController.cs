using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineForum.Core.Interfaces;
using OnlineForum.Web.Utility;
using OnlineForum.Web.ViewModels.Users;

namespace OnlineForum.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult User(int userId)
        {
            var user = _userService.GetUser(userId);

            if (user != null)
            {
                var threads = _userService.GetUserThreads(userId);
                var comments = _userService.GetUserComments(userId);

                var userViewModel = new UserViewModel()
                {
                    User = user,
                    Threads = threads,
                    Comments = comments
                };

                return View(userViewModel);
            }

            return RedirectToAction("Index", "Forum");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View(_userService.GetUsers());
        }
    }
}
