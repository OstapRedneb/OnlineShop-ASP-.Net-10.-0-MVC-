using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class CalcController : Controller
    {
        public string Index(double a, double b, string c)
        {
            return c switch 
            {
                null or "+" => (a + b).ToString(),
                "-" => (a - b).ToString(),
                "*" => (a * b).ToString(),
                "/" => b == 0 ? "На ноль делить нельзя!" : (a / b).ToString(),
                _ => "ошибка в вычислении"
            };
        }
    }
}
