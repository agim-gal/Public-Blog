using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public_Blog.Models
{
    public static class AccountRepository
    {
        //Метод проверяет, не заняты ты ли логин и email
        //должен вызываться перед RegisterUser, если вернётся что-то кроме Nope
        //то RegisterUser кинет исключение
        public static RegisterValidError RegisterValid(RegisterModel user)
        {
            //создаём перечисление для регистрации возможных ошибок
            RegisterValidError result = new RegisterValidError();

            //изначально инициируем его "ошибок нет"
            result = RegisterValidError.Nope;

            using (DBContext db = new DBContext())
            {
                //если в базе есть запись с таким логином
                bool LoginBusy = db.Users.Where(u => u.Login == user.Login).Any();
                if (LoginBusy)
                {
                    //то добавляем к возвращаему результату флаг логин занят
                    result = result | RegisterValidError.LoginBusy;
                }

                //если в базе есть запись с таким email
                bool EmailBusy = db.Users.Where(u => u.Email == user.Email).Any();
                if (EmailBusy)
                {
                    //то добавляем к возвращаему результату флаг email занят
                    result = result | RegisterValidError.EmailBusy;
                }
                
            }

            return result;
        }

        //Метод добавляем пользователя в базу
        public static void RegisterUser(RegisterModel user)
        {
            //если данные валидны(логин и email не заняты)
            if ((RegisterValid(user) & RegisterValidError.AllErrors) == RegisterValidError.Nope)
            {
                using (DBContext db = new DBContext())
                {
                    //создаём запись о новом пользователе 
                    User newUser = new User();
                    newUser.Login = user.Login;
                    newUser.Email = user.Email;
                    newUser.Password = user.Password;

                    //и добавляем её в базу
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return;
                }
            }
            //а иначе ловите исключение
            else
            {
                throw new RegiserFailException(RegisterValid(user).ToString());
            }
            
        }

        //Метод проверяет логин и пароль, 
        //возвращает true, если в базе есть запись с соответсвующими значениями 
        public static bool AuthorizationDataValid(string login, string password)
        {
            using (DBContext db = new DBContext())
            {
                bool userExist = db.Users.Where(u => u.Login == login && u.Password == password).Any();
                return userExist?true:false;
            }
        }

        //тоже самое, только в профиль
        public static bool AuthorizationDataValid(AuthorizationModel user)
        {
            return AuthorizationDataValid(user.Login, user.Password);
        }

    }
    

    [Flags]
    public enum RegisterValidError
    {
        Nope = 0,
        LoginBusy = 1,
        EmailBusy = 2,
        AllErrors = LoginBusy | EmailBusy
    }

    [Serializable]
    public class RegiserFailException : Exception
    {
        public RegiserFailException() { }
        public RegiserFailException(string message) : base(message) { }
        public RegiserFailException(string message, Exception inner) : base(message, inner) { }
        protected RegiserFailException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
