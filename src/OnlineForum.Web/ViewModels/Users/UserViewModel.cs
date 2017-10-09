using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineForum.Core.Models;

namespace OnlineForum.Web.ViewModels.Users
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Core.Models.Thread> Threads { get; set; }
        public IEnumerable<Core.Models.CommentNode> Comments { get; set; }
    }
}
