using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Public_Blog.Models;
using System.Reflection;

namespace Public_Blog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            DataPaging dataPaging = new DataPaging()
            {
                controller = "Home",
                action = "Index",
                parameter = "",
                page = page,
                maxPage = ContentRepository.getCountPage()
            };
            ViewBag.dataPaging = dataPaging;

            return View(ContentRepository.getPosts(page));
        }
    }
}