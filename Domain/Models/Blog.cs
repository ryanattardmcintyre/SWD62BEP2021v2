using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Blog
    {
        [Key] //sets this as primary key
              //sets this as an identity
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        
        [ForeignKey("Category")]
        public int CategoryId { get; set; } //foreignkey
        public virtual Category Category { get; set; } //navigational property: facilitates the way you can access data from other tables
        public string LogoImageUrl { get; set; }

    }
}
