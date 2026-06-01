using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class CalculatorController : Controller
    {
        public int Index(int firstNumber, int secondNumber) => firstNumber + secondNumber;
    }
}
