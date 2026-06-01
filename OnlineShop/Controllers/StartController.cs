using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class StartController : Controller
    {
        public string Hello()
        {
            DateTime time = DateTime.Now;

            if (time.Hour < 6 && time.Hour >= 0)
                return "Доброй ночи";
            if (time.Hour >= 6 && time.Hour < 12)
                return "Доброе утро";
            if (time.Hour >= 12 && time.Hour < 18)
                return "Добрый день";
            if (time.Hour >= 18)
                return "Добрый вечер";

            return "Ошибка определения времени";
        }
    }
}
