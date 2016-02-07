using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Public_Blog.Models
{
    public class User
    {
        [Key]
        public int Id_user { get; set; }

        [MaxLength(30)]
        public string Login { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}