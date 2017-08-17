using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OnlineForum.Core.Implementations;
using OnlineForum.Core.Models;
using OnlineForum.DAL;

namespace OnlineForum.Core.Tests
{
    [TestFixture]
    public class CommentServiceTest : TestBase
    {
        [Test]
        public void CommentTreeBuiltSuccessful()
        {
            using (var context = GetNewContext())
            {
                var commentService = GetCommentService(context);

                var commentNodes = commentService.GetComments(1);

                var topComment = commentNodes.First().Comment;
                var topCommentReplyOne = commentNodes.First().ChildComments.First().Comment;
                var topCommentReplyTwo = commentNodes.First().ChildComments.Last().Comment;
                var topCommentReplyTwoReplyOne = commentNodes.First().ChildComments.Last().ChildComments.First().Comment;

                Assert.AreEqual(topComment.Content, "Top Comment");
                Assert.AreEqual(topCommentReplyOne.Content, "Top Comment Reply 1");
                Assert.AreEqual(topCommentReplyTwo.Content, "Top Comment Reply 2");
                Assert.AreEqual(topCommentReplyTwoReplyOne.Content, "Top Comment Reply 2 Reply 1");

            }
        }

        private static CommentService GetCommentService(OnlineForumContext context)
        {
            var mapper = GetMapper();

            var commentService = new CommentService(context, mapper);

            return commentService;
        }
    }
}
