using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BlogsController : Controller
    {
        private IBlogsService blogsService;
        private ICategoriesService categoriesService;
        public BlogsController(IBlogsService _blogsService, ICategoriesService _categoriesService)
        { blogsService = _blogsService;
            categoriesService = _categoriesService;
        }

        public IActionResult Index()
        {
            var list = blogsService.GetBlogs();
            
            return View(list);
        }

        //this is going to load the page; this runs before the page is rendered
        [HttpGet]
        public IActionResult Create ()
        {
            ViewBag.Categories = categoriesService.GetCategories();
            return View();
        }
           
        [HttpPost]
        public IActionResult Create(AddBlogViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blogsService.AddBlog(model);
                    ViewBag.Message = "Blog added successfully";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Blog wasn't added successfully";
            }
           
            ViewBag.Categories = categoriesService.GetCategories();
            return View();
        }
    }
}
