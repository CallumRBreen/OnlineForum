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
    public class Comment : ViewComponent
    {
        public IViewComponentResult Invoke(Core.Models.CommentNode commentNode, bool ShowFullContent)
        {
            ViewBag.ShowFullContent = ShowFullContent;

            SetViewBagIfUserVoteExists(commentNode);

            return View(commentNode);
        }

        private void SetViewBagIfUserVoteExists(Core.Models.CommentNode commentNode)
        {
            ViewBag.HasUserUpvoted = false;
            ViewBag.HasUserDownvoted = false;

            var userId = HttpContext.GetCurrentUserId();

            if (userId != 0)
            {
                var vote = commentNode.Comment.Votes?.FirstOrDefault(v => v.VoteBy.UserId == userId);

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
