using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineForum.Web.ViewModels.Comment
{
    public class AddCommentViewModel
    {
        public int ThreadId { get; set; }
        public int ParentId { get; set; }
        [Required(ErrorMessage = "Please enter some content.")]
        public string Content { get; set; }
    }
}
