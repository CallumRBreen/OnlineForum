using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineForum.Core.Models
{
    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
