using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.Web.ViewModels;
using OnlineForum.Web.ViewModels.Account;

namespace OnlineForum.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index() => View(_userService.GetUsers());

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(Login userLogin)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.SignIn(userLogin.Username, userLogin.Password);

                if (result)
                {
                    // do cookie stuff
                }
                else
                {
                    RedirectToAction("Forbidden");
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateUser(user.UserName, user.Password, user.Email);

                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Forbidden() => View();
    }
}
