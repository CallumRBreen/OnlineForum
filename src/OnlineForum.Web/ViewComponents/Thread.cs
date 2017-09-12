using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.Web.Utility;


namespace OnlineForum.Web.ViewComponents
{
    public class Thread : ViewComponent
    {
        private readonly IUserService _userService;

        public Thread(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke(Core.Models.Thread thread, bool ShowFullContent)
        {
            ViewBag.ShowFullContent = ShowFullContent;

            SetViewBagIfUserVoteExists(thread);

            return View(thread);
        }

        private void SetViewBagIfUserVoteExists(Core.Models.Thread thread)
        {
            ViewBag.HasUserUpvoted = false;
            ViewBag.HasUserDownvoted = false;

            var userId = HttpContext.GetCurrentUserId();

            if (userId != 0)
            {
                var vote = thread.Votes?.FirstOrDefault(v => v.VoteBy.UserId == userId);

                if (vote != null)
                {
                    switch (vote.VoteScore)
                    {
                        case 1:
                            ViewBag.HasUserUpvoted = true;
                            break;
                        case -1:
                            ViewBag.HasUserDownvoted = true;
                            break;
                    }
                }
            }
        }
    }
}
