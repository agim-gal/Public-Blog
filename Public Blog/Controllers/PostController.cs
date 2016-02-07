using Public_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;


namespace Public_Blog.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(int id)
        {
            
            using (DBContext db = new DBContext())
            {
                try
                {
                    //пробуем получаем пост по id
                    PostView postView = ContentRepository.getPost(id);
                    return View(postView);
                }
                //если не получилось, то возвращаем страницу 404
                catch
                {
                    return View("Error404");
                }    
            }
        }

        

    }
}