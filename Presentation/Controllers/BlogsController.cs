﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BlogsController : Controller
    {
        private IBlogsService blogsService;
        private ICategoriesService categoriesService;
        private IWebHostEnvironment hostEnv;
        public BlogsController(IBlogsService _blogsService, ICategoriesService _categoriesService,
            IWebHostEnvironment _hostEnv)
        { blogsService = _blogsService;
            categoriesService = _categoriesService;
            hostEnv = _hostEnv;
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
        public IActionResult Create(AddBlogViewModel model, IFormFile logoFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (logoFile != null)
                    {
                        //1. to generate a new unique filename
                        //5389205C-813B-4AFA-A453-B912C30BF933.jpg
                        string newFilename = Guid.NewGuid() + Path.GetExtension(logoFile.FileName);

                        //2. find what the absolute path to the folder Files is
                        //C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\Files\5389205C-813B-4AFA-A453-B912C30BF933.jpg

                        //hostEnv.ContentRootPath : C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation
                        //hostEnv.WebRootPath:  C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\wwwroot

                        string absolutePath = hostEnv.WebRootPath + "\\Files";
                        string absolutePathWithFilename = absolutePath + "\\" + newFilename;
                        model.LogoImageUrl = "\\Files\\" + newFilename;
                        //3. do the transfer/saving of the actual physical file

                        using (FileStream fs = new FileStream(absolutePathWithFilename, FileMode.CreateNew, FileAccess.Write))
                        {
                            logoFile.CopyTo(fs);
                            fs.Close();
                        }
                    }

                
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


        public IActionResult Details(int id)
        {
            var blog = blogsService.GetBlog(id);
            return View(blog);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                blogsService.DeleteBlog(id);
                TempData["Message"] = "Blog was deleted successfully";

                //ViewBag/ViewData which does not survive a redirection
                //TempData which survives a redirection
                //Session (ControllerContext.HttpContext.Session) which survives all redirections

            }
            catch (Exception ex)
            {
                TempData["Error"] = "Blog wasnt deleted. Error: " + ex.Message;

            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var blogToBeEdited= blogsService.GetBlog(id);
            return View(blogToBeEdited); 
        }

        public IActionResult Update(BlogViewModel model, IFormFile logoFile)
        {
            try
            {
                var originalBlog = blogsService.GetBlog(model.Id);

                if (ModelState.IsValid)
                {
                    if (logoFile != null)
                    {
                        //deleting only if there is a new image uploading
                        System.IO.File.Delete(hostEnv.WebRootPath + originalBlog.LogoImageUrl);

                        //1. to generate a new unique filename
                        //5389205C-813B-4AFA-A453-B912C30BF933.jpg
                        string newFilename = Guid.NewGuid() + Path.GetExtension(logoFile.FileName);

                        //2. find what the absolute path to the folder Files is
                        //C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\Files\5389205C-813B-4AFA-A453-B912C30BF933.jpg

                        //hostEnv.ContentRootPath : C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation
                        //hostEnv.WebRootPath:  C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\wwwroot

                        string absolutePath = hostEnv.WebRootPath + "\\Files";
                        string absolutePathWithFilename = absolutePath + "\\" + newFilename;
                        model.LogoImageUrl = "\\Files\\" + newFilename;
                        //3. do the transfer/saving of the actual physical file

                        using (FileStream fs = new FileStream(absolutePathWithFilename, FileMode.CreateNew, FileAccess.Write))
                        {
                            logoFile.CopyTo(fs);
                            fs.Close();
                        }
                    }

                    AddBlogViewModel myModel = new AddBlogViewModel();
                    myModel.LogoImageUrl =  model.LogoImageUrl;
                    myModel.Name = model.Name;
                    
                    //LazyLoading needs to be applied  here
                    myModel.CategoryId = originalBlog.Category.Id; //note change this so that the user submits the category after selecting it

                    //category is not being updated since i did not include the select in page

                    blogsService.UpdateBlog(myModel, model.Id);
                    ViewBag.Message = "Blog updated successfully";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Blog wasn't updated successfully";
            }

            ViewBag.Categories = categoriesService.GetCategories();
            return View();
        }

    }
}
