using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Public_Blog.Models;

namespace Public_Blog.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult GetImage(int id)
        {

            Image image = new Image();
            //запрашиваем изображение из базы
            using(DBContext db = new DBContext())
            {
                image = ContentRepository.getImage(id);
            }
            //если такое есть, то возвращаем изображение
            if (image != null)
            {
                return File(image.ImageData, image.ImageMimeType);
            }
            //на нет и суда нет
            else
            {
                return Content("Изображение отсутствует");
            }
        }


    }
}