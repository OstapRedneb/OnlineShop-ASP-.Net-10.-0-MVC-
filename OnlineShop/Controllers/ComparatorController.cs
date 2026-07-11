using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Controllers
{
    public class ComparatorController(IProductService productService, IComparatorService comparatorService) : Controller
    {
        public IActionResult Index()
        {
            Comparator? comparator = comparatorService.GetById(Info.Info.CommonComparatorId);

            if (comparator is null)
            {
                comparator = new Comparator(Info.Info.CommonComparatorId);
                comparatorService.Add(comparator);
            }

            return View(comparator);
        }
        public IActionResult Add(Guid productId)
        {
            Comparator? comparator = comparatorService.GetById(Info.Info.CommonComparatorId);
            Product? product = productService.GetById(productId);

            if (comparator is null)
            {
                comparator = new Comparator() { Id = Info.Info.CommonComparatorId };
                comparatorService.Add(comparator);
            }

            if (comparator.Add(product))
                comparatorService.Update(comparator);

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);
        }
        public IActionResult Remove(Guid productId) 
        {
            Comparator? comparator = comparatorService.GetById(Info.Info.CommonComparatorId);
            Product? product = productService.GetById(productId);

            if (comparator is null)
            {
                comparator = new Comparator() { Id = Info.Info.CommonComparatorId };
                comparatorService.Add(comparator);
            }

            if (comparator.Remove(product))
                comparatorService.Update(comparator);

            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            Comparator? comparator = comparatorService.GetById(Info.Info.CommonComparatorId);

            if (comparator is null)
            {
                comparator = new Comparator() { Id = Info.Info.CommonComparatorId };
                comparatorService.Add(comparator);
            }

            comparator.Clear();
            comparatorService.Update(comparator);

            return RedirectToAction("Index");
        }
    }
}
