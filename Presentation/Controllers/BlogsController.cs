using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BlogsController : Controller
    {
        private IBlogsService blogsService;
        public BlogsController(IBlogsService _blogsService)
        { blogsService = _blogsService; }

        public IActionResult Index()
        {
            var list = blogsService.GetBlogs();
            
            return View(list);
        }
    }
}
