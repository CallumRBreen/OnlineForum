using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineForum.Core.Interfaces;
using OnlineForum.Core.Models;
using OnlineForum.DAL;
using OnlineForum.DAL.Entities;
using Comment = OnlineForum.Core.Models.Comment;
using CommentVote = OnlineForum.DAL.Entities.CommentVote;
using Thread = OnlineForum.Core.Models.Thread;
using User = OnlineForum.Core.Models.User;

namespace OnlineForum.Core.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly OnlineForumContext _context;
        private readonly IMapper _mapper;

        public CommentService(OnlineForumContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateComment(string content, Comment parentComment, User user, Thread thread)
        {
            var entityComment = _mapper.Map<DAL.Entities.Comment>(parentComment);
            var entityUser = _mapper.Map<DAL.Entities.User>(user) ?? throw new Exception("User cannot be null");
            var entityThread = _mapper.Map<DAL.Entities.Thread>(thread) ?? throw new Exception("Thread cannot be null");

            var newComment = new DAL.Entities.Comment()
            {
                Thread = entityThread,
                User = entityUser,
                Content = content,
                Parent = entityComment,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Votes = new List<CommentVote>()
            };

            _context.Comments.Add(newComment);

            var comment = new DAL.Entities.CommentVote()
            {
                VoteScore = 1,
                VoteBy = entityUser
            };

            newComment.Votes.Add(comment);

            _context.SaveChanges();
        }

        public IEnumerable<CommentNode> GetCommentsAsNodes(int threadId)
        {
            var comments = _context.Comments.Include(x => x.Thread)
                                            .Include(x => x.Parent)
                                            .Include(x => x.User)
                                            .Include(x => x.Votes).ThenInclude(x => x.VoteBy)
                                            .Where(x => x.Thread.ThreadId == threadId).ToList();

            var modelComments = _mapper.Map<List<Comment>>(comments);

            var commentNodes = modelComments.Select(x => new CommentNode {Comment = x}).ToList();

            var childCommentHashes = commentNodes.ToLookup(x => x.Comment?.Parent?.CommentId);

            foreach (var commentNode in commentNodes)
            {
                commentNode.ChildComments = childCommentHashes[commentNode.Comment.CommentId].ToList();
            }
  
            return commentNodes.Where(x => x.Comment.Parent == null);
        }

        public Comment GetComment(int commentId)
        {
            var comment = _context.Comments.Include(x => x.Thread)
                                            .Include(x => x.Parent)
                                            .Include(x => x.User)
                                            .Include(v => v.Votes).ThenInclude(u => u.VoteBy)
                                            .FirstOrDefault(x => x.CommentId == commentId);

            if (comment == null) return null;

            return _mapper.Map<Comment>(comment);
        }

        public VoteResult Upvote(int commentId, int userId)
        {
            var comment = _context.Comments.Include(x => x.Thread)
                                           .Include(x => x.Parent)
                                           .Include(x => x.User)
                                           .Include(v => v.Votes).ThenInclude(u => u.VoteBy)
                                           .FirstOrDefault(x => x.CommentId == commentId);

            var user = _context.Users.First(x => x.UserId == userId);

            var vote = comment.Votes.FirstOrDefault(x => x.VoteBy.UserId == user.UserId);

            if (vote == null)
            {
                vote = new DAL.Entities.CommentVote
                {
                    VoteScore = 1,
                    VoteBy = user

                };

                comment.Votes.Add(vote);
            }
            else
            {
                vote.VoteScore = vote.VoteScore == 1 ? 0 : 1;
            }

            _context.SaveChanges();

            return new VoteResult()
            {
                Score = comment.Votes.Sum(x => x.VoteScore),
                DidScoreIncrease = vote.VoteScore == 1,
            };
        }

        public VoteResult Downvote(int commentId, int userId)
        {
            var comment = _context.Comments.Include(x => x.Thread)
                                            .Include(x => x.Parent)
                                            .Include(x => x.User)
                                            .Include(v => v.Votes).ThenInclude(u => u.VoteBy)
                                            .FirstOrDefault(x => x.CommentId == commentId);

            var user = _context.Users.First(x => x.UserId == userId);

            var vote = comment.Votes.FirstOrDefault(x => x.VoteBy.UserId == user.UserId);

            if (vote == null)
            {
                vote = new DAL.Entities.CommentVote
                {
                    VoteScore = -1,
                    VoteBy = user

                };

                comment.Votes.Add(vote);
            }
            else
            {
                vote.VoteScore = vote.VoteScore == -1 ? 0 : -1;
            }

            _context.SaveChanges();

            return new VoteResult()
            {
                Score = comment.Votes.Sum(x => x.VoteScore),
                DidScoreIncrease = vote.VoteScore == 0
            };
        }
    }
}
