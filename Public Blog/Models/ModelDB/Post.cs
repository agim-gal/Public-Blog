using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Public_Blog.Models
{
    public class Post
    {
        [Key]
        public int Id_post { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Header { get; set; }
        public string Text { get; set; }

        [Column(TypeName = "image")]
        public virtual Image Image { get; set; }

        
        public virtual ICollection<Tag> Tags { get; set; }

        public Post()
        {
            Tags = new List<Tag>();
        }
    }
}