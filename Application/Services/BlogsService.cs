using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class BlogsService : IBlogsService
    {
        private IBlogsRepository blogsRepo;
        public BlogsService(IBlogsRepository _blogsRepo)
        {
            blogsRepo = _blogsRepo;
        }
        public void AddBlog(AddBlogViewModel model)
        {
            blogsRepo.AddBlog(
                new Domain.Models.Blog()
                {
                     CategoryId = model.CategoryId,
                     Name = model.Name,
                     LogoImageUrl = model.LogoImageUrl,
                     DateCreated = DateTime.Now,
                     DateUpdated = DateTime.Now
                });

        }

        public void DeleteBlog(int id)
        {
            var blog = blogsRepo.GetBlog(id);
            if (blog == null)
                throw new Exception("The Blog does not exist");
            else
            {
                blogsRepo.DeleteBlog(blog);
            }
        }

        public BlogViewModel GetBlog(int id)
        {
            var blog = blogsRepo.GetBlog(id);
            var result = new BlogViewModel()
            {
                Id = blog.Id,
                Category = blog.Category,
                DateUpdated = blog.DateUpdated,
                LogoImageUrl = blog.LogoImageUrl,
                Name = blog.Name
            };
            return result;
        }

        public IQueryable<BlogViewModel> GetBlogs()
        {
            //all this will be changed into 1 line with the introduction of AutoMapper

            var list = from b in blogsRepo.GetBlogs() //List<Blog>
                       select new BlogViewModel()
                       {
                           Category = b.Category,
                           DateUpdated = b.DateUpdated,
                           LogoImageUrl = b.LogoImageUrl,
                           Id = b.Id,
                           Name = b.Name
                       };
            return list;
        }

        public void UpdateBlog(AddBlogViewModel editedDetails, int id)
        {
            var originalBlog = blogsRepo.GetBlog(id);
            originalBlog.CategoryId = editedDetails.CategoryId;
            originalBlog.LogoImageUrl = editedDetails.LogoImageUrl;
            originalBlog.Name = editedDetails.Name;

            blogsRepo.UpdateBlog(originalBlog);
        }
    }
}
