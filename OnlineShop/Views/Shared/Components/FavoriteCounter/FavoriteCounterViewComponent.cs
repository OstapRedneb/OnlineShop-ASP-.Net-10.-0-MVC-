using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Views.Shared.Components.FavoriteCounter
{
    public class FavoriteCounterViewComponent(IFavoriteService favoriteService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Favorite? favorite = favoriteService.GetById(Info.Info.CommonFavoriteId);

            int answer = favorite is null ? 0 : favorite.Count;

            return View("FavoriteCounter", answer);
        }
    }
}
