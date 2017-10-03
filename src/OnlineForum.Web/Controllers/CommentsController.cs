using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.Web.Utility;
using OnlineForum.Web.ViewModels.Comment;

namespace OnlineForum.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IThreadService _threadService;

        public CommentsController(ICommentService commentService, IUserService userService, IThreadService threadService)
        {
            _commentService = commentService;
            _userService = userService;
            _threadService = threadService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(int threadId)
        {
            var thread = _threadService.GetThread(threadId); // Add 404 page if null

            if (thread != null)
            {
                var commentsViewModel = new CommentsViewModel()
                {
                    Thread = thread,
                    Comments = _commentService.GetCommentsAsNodes(threadId)

                };

                return View(commentsViewModel);
            }

            return RedirectToAction("Index", "Forum");
        }

        [HttpGet]
        public IActionResult Add(int parentId, int threadId)
        {
            return View(new AddCommentViewModel()
            {
                ThreadId = threadId,
                ParentId = parentId
            });
        }

        [HttpPost]
        public IActionResult Add(AddCommentViewModel addCommentVm)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                var parentComment = _commentService.GetComment(addCommentVm.ParentId);
                var user = _userService.GetUser(HttpContext.GetCurrentUserId());
                var thread = _threadService.GetThread(addCommentVm.ThreadId);

                _commentService.CreateComment(addCommentVm.Content, parentComment, user, thread);
                
                return RedirectToAction("Index", new {threadId = addCommentVm.ThreadId});
            }

            return View();
        }

        [HttpPost]
        public JsonResult Upvote(int commentId)
        {
            var voteResult = _commentService.Upvote(commentId, HttpContext.GetCurrentUserId());

            return new JsonResult(voteResult);
        }

        [HttpPost]
        public JsonResult Downvote(int commentId)
        {
            var voteResult = _commentService.Downvote(commentId, HttpContext.GetCurrentUserId());

            return new JsonResult(voteResult);
        }  
    }
}
