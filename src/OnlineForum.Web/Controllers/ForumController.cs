using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;

namespace OnlineForum.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IThreadService _threadService;

        public ForumController(IThreadService threadService)
        {
            _threadService = threadService;
        }

        public IActionResult Index()
        {
            return View(_threadService.GetThreads());
        }

        public IActionResult Edit(int threadId)
        {
            return View(_threadService.GetThread(threadId));
        }

        [HttpPost]
        public IActionResult Edit(Thread thread)
        {
            if (ModelState.IsValid)
            {
                thread.Modified = DateTime.Now;
                _threadService.EditThread(thread);
                return RedirectToAction("Index");
            }
            else
            {
                return View(thread);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Thread thread)
        {
            if (ModelState.IsValid)
            {
                thread.Created = DateTime.Now;
                thread.Modified = DateTime.Now;
                thread.Downvotes = 0;
                thread.Upvotes = 0;
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
    }
}
