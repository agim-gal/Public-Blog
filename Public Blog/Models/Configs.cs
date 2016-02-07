using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Public_Blog.Models
{
    public class Configs
    {
        //Глобальное свойство, описывает сколько постов на странице следует размещать 
        internal static int countPostsOnPage()
        {
            int defaultValue = 10;
            try
            {
                //запрашивает данные из конфига
                //если там этих данных нет, то вернётся defaultValue
                return Convert.ToInt32(ConfigurationManager.AppSettings["countPostsOnPage"] ?? defaultValue.ToString());
            }
            catch (Exception)
            {
                //а если эти не корректны, то тоже вернём defaultValue
                return defaultValue;
            }
        }
    }
}