using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    //ctrl + . >>suggests the namespace/library which you need to add

    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateUpdated { get; set; }

        public Category Category { get; set; }

        public string LogoImageUrl { get; set; }
    }
}
