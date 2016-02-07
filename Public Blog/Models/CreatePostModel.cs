using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Public_Blog.Models
{
    public class CreatePostModel
    {
        [Required]
        [Display(Name = "Заголовок")]
        public string Header { get; set; }

        [Display(Name = "Теги")]
        [Required(ErrorMessage = "Введите миниму 1 тег")]
        public string Tags { get; set; }

        [Display(Name = "")]
        public string Text { get; set; }

    }

}