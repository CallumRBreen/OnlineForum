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
                Upvotes = 0,
                Downvotes = 0,
                Parent = entityComment,
                Created = DateTime.Now,
                Modified = DateTime.Now 
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();
        }

        public IEnumerable<CommentNode> GetComments(int threadId)
        {
            var comments = _context.Comments.Include(x => x.Thread)
                                            .Include(x => x.Parent)
                                            .Include(x => x.User)
                                            .Where(x => x.Thread.ThreadId == threadId);

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
                                            .FirstOrDefault(x => x.CommentId == commentId);

            if (comment == null) return null;

            return _mapper.Map<Comment>(comment);
        }
    }
}
