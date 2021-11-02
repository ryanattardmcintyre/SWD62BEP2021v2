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

        public BlogViewModel GetBlog(int id)
        {
            throw new NotImplementedException();
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
    }
}
