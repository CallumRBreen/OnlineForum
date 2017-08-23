using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineForum.Core.Interfaces;
using OnlineForum.Web.Utility;

namespace OnlineForum.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index() => View(_userService.GetUsers());

        [HttpGet]
        public IActionResult Profile()
        {
            var user = _userService.GetUser(HttpContext.GetCurrentUserId());

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Login", "Account");
        }

    }
}
