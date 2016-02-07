using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public_Blog.Models
{
    public class PostView
    {
        public int id_post;

        public string Header;

        public string username;

        public List<string> tags;

        public string textContent;

        public int id_image;

    }
}