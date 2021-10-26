using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{

    //role:
    //connecting with the db
    //executing one operation at a time
    public class BlogsRepositories : IBlogsRepository
    {
        private BloggingContext context;

        public BlogsRepositories(BloggingContext _context)
        {
            context = _context;
        }
        public void AddBlog(Blog b)
        {
            context.Blogs.Add(b);
            context.SaveChanges();
        }
        public void DeleteBlog(Blog b)
        {
            context.Blogs.Remove(b);
            context.SaveChanges();
        }
        public Blog GetBlog(int id)
        {
            return context.Blogs.SingleOrDefault(b => b.Id == id);

            //foreach (Blog b in context.blogs)
            //    if (b.Id == id)
            //        return b;

            //var list = from b in context.Blogs
            //           select b;
            //return list.FirstOrDefault();
        }
        public IQueryable<Blog> GetBlogs()
        {
            return context.Blogs;

            //var list = from b in context.Blogs
            //           select b;
            //return list;
        }
        public void UpdateBlog(Blog b)
        {
            var originalBlog = GetBlog(b.Id);
            originalBlog.DateUpdated = DateTime.Now;
            originalBlog.Name = b.Name;
            originalBlog.LogoImageUrl = b.LogoImageUrl;
            originalBlog.CategoryId = b.CategoryId;
            context.SaveChanges();
        }
    }
}
