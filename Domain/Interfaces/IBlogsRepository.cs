using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IBlogsRepository
    {
        public IQueryable<Blog> GetBlogs();
        public Blog GetBlog(int id);
        public void AddBlog(Blog b);

        public void DeleteBlog(Blog b);

        public void UpdateBlog(Blog b);

      
    }
}
