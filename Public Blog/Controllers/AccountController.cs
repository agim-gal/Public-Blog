using Public_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Public_Blog.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            RegisterValidError error = AccountRepository.RegisterValid(model);

            //если логин занят, то добавляем ModelState соответствующее сообщение 
            if (error.HasFlag(RegisterValidError.LoginBusy))
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
            }

            //если email занят, то добавляем ModelState соответствующее сообщение 
            if (error.HasFlag(RegisterValidError.EmailBusy))
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже существует");
            }

            //если никаких ошибок нет
            if (ModelState.IsValid)
            {
                //создаём новый аккаунт
                AccountRepository.RegisterUser(model);

                //на всякий случай проверяем, добавились ли данные
                if (AccountRepository.AuthorizationDataValid(model.Login, model.Password))
                {
                    //выдаём куки
                    FormsAuthentication.SetAuthCookie(model.Login, true);

                    //и перенаправляем на главную
                    return RedirectToAction("Index", "Home");
                }
            }
            
            //в любой непонятной ситуации опять показываем страницу авторизации 
            return View("Register");
        }

        [HttpGet]
        public ActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(AuthorizationModel model, string ReturnUrl)
        {
            //если данные валидны
            if (AccountRepository.AuthorizationDataValid(model))
            {
                //авторизуем пользователя
                FormsAuthentication.SetAuthCookie(model.Login, true);

                IPrincipal d = User;
                //перенаправляем его на главную 
                return RedirectToAction("Index", "Home");
            }
            //а иначе назад к форме авторизации
            else
            {
                ModelState.AddModelError("", "Неверый логин и/или пароль");
                return View("Authorization");
            }   
        }

        public ActionResult LogOut()
        {
            //удаляем куки
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Index", "Home"));
        }
    }
}