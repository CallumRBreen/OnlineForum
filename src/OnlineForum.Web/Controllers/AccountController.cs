using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;

namespace OnlineForum.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login(User user)
        {
            var result = _userService.SignIn(user.UserName, user.Password);

            if (result)
            {
                // do cookie stuff
            }
            else
            {
                // show error message;
            }

            return View("Index");
        }
    }
}
