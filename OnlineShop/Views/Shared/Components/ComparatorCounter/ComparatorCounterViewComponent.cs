
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Views.Shared.Components.ComparatorCounter
{
    public class ComparatorCounterViewComponent(IComparatorService comporatorService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Comparator? comporator = comporatorService.GetById(Info.Info.CommonComparatorId);

            int result = comporator?.Count ?? 0;

            return View("ComporatorCounter", result);
        }
    }
}
