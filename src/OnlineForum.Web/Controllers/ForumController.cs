using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.Web.Utility;


namespace OnlineForum.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IThreadService _threadService;
        private readonly IUserService _userService;

        public ForumController(IThreadService threadService, IUserService userService)
        {
            _threadService = threadService;
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_threadService.GetThreads().OrderByDescending(t => t.GetScore()));
        }

        [HttpGet]
        public IActionResult Edit(int threadId) => View(_threadService.GetThread(threadId));

        [HttpPost]
        public IActionResult Edit(Thread thread)
        {
            if (ModelState.IsValid)
            {
                _threadService.EditThread(thread);
                return RedirectToAction("Index");
            }
            else
            {
                return View(thread);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Thread thread)
        {
            if (ModelState.IsValid)
            {
                thread.User = _userService.GetUser(HttpContext.GetCurrentUserId());

                _threadService.CreateThread(thread);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(int threadId)
        {
            _threadService.DeleteThread(threadId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Upvote(int threadId)
        {
            _threadService.Upvote(threadId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Downvote(int threadId)
        {
            _threadService.Downvote(threadId);
            return RedirectToAction("Index");
        }

        
    }
}
