using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class CategoriesService: ICategoriesService
    {
        private ICategoriesRepository categoriesRepo;
        public CategoriesService(ICategoriesRepository _categoriesRepo)
        {
            categoriesRepo = _categoriesRepo;
        }

        public IQueryable<Category> GetCategories()
        {
            return categoriesRepo.GetCategories();
        }
    }
}
