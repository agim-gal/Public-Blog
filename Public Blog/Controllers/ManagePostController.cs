using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Public_Blog.Models;

namespace Public_Blog.Controllers
{
    public class ManagePostController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreatePostModel post, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                ContentRepository.addPost(post, image, User.Identity.Name);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Remove(int id = 0)
        {
            try
            {
                ContentRepository.removePost(id, User.Identity.Name);
                return Json(new { request = "ok", idPost = id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { request = "error", idPost = id }, JsonRequestBehavior.AllowGet);
            }
            
            
            /*if (id == 0)
            {
                return Content("Error");
            }                                                                                                                                                                                                                                                                                                           ```

            return Content("Ok");*/
        }
    }
}