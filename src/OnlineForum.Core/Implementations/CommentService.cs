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

        public void CreateComment(Comment comment)
        {
            throw new NotImplementedException();
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
    }
}
