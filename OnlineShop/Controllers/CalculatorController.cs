using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class CalculatorController : Controller
    {
        public int Index(int firstNumber, int secondNumber, string operationChar)
        {
            return operationChar switch 
            {
                null or "+" => firstNumber + secondNumber,
                "-" => firstNumber - secondNumber,
                "*" => firstNumber * secondNumber,
                _ => 0
            };
        }
    }
}
