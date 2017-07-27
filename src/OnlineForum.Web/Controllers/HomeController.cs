using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineForum.Core.Interfaces;

namespace OnlineForum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IThreadService _threadService;

        public HomeController(IThreadService threadService)
        {
            _threadService = threadService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(_threadService.GetThreads());
        }
    }
}
