using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Public_Blog.Models
{
    public class Image
    {
        [Key]
        public int Id_image { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public bool IsUsed { get; set; }
    }
}