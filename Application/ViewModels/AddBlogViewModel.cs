using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class AddBlogViewModel
    {
        [Required]
        public string Name { get; set; }
        public string LogoImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
