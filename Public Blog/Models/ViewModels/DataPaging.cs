using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public_Blog.Models
{
    public class DataPaging
    {
        public string controller { get; set; }
        public string action { get; set; }
        public string parameter { get; set; }
        public int page { get; set; }
        public int maxPage { get; set; }
    }
}