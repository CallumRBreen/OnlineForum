using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineForum.Core.Models;

namespace OnlineForum.Web.ViewModels
{
    public class ThreadComponentViewModel
    {
        public bool ExpandComment { get; set; } = false;
        public Thread Thread { get; set; }
    }
}
