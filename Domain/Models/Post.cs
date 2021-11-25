using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }

        public string Author { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

    }
}
