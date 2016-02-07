using Public_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public_Blog.Controllers
{
    public class PostsController : Controller
    {
        //parameter - в данном случае имя пользователя
        public ActionResult User(string parameter, int page)
        {

            DataPaging dataPaging = new DataPaging()
            {
                controller = "Posts",
                action = "User",
                parameter = parameter,
                page = page,
                maxPage = ContentRepository.getCountPageByUser(parameter)
            };
            ViewBag.dataPaging = dataPaging;

            return View(ContentRepository.getPostsByUser(parameter, page));
        }

        //parameter - в данном случае тег
        public ActionResult Tag(string parameter, int page)
        {
            DataPaging dataPaging = new DataPaging()
            {
                controller = "Posts",
                action = "Tag",
                parameter = parameter,
                page = page,
                maxPage = ContentRepository.getCountPageByTag(parameter)
            };
            ViewBag.dataPaging = dataPaging;

            return View(ContentRepository.getPostsByTag(parameter, page));
        }

    }
}