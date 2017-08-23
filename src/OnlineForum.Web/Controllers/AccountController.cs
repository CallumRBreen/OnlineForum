﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.Web.ViewModels;
using OnlineForum.Web.ViewModels.Account;

namespace OnlineForum.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index() => RedirectToAction("Index", "Forum");

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.SignIn(userLogin.Username, userLogin.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLogin.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                    };
 
                    ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));
                    await HttpContext.Authentication.SignInAsync("CookieAuthentication", principal);

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Forbidden");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("CookieAuthentication");
            return RedirectToAction("Login");
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
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
