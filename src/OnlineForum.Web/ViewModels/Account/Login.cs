using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineForum.Web.ViewModels.Account
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
