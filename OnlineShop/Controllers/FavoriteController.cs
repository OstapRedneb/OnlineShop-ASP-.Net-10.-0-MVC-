using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Controllers
{
    public class FavoriteController(IProductService productService, ICartService cartService, IFavoriteService favoriteService) : Controller
    {
        public IActionResult Index()
        {
            Favorite? favorite = favoriteService.GetById(Info.Info.CommonFavoriteId);

            if (favorite is null)
            {
                favorite = new Favorite(Info.Info.CommonFavoriteId);
                favoriteService.Add(favorite);
            }

            return View(favorite);
        }
        public IActionResult Add(Guid productId)
        {
            Favorite? favorite = favoriteService.GetById(Info.Info.CommonFavoriteId);
            Product? product = productService.GetById(productId);

            if (favorite is null)
            {
                favorite = new Favorite() { Id = Info.Info.CommonFavoriteId };
                favoriteService.Add(favorite);
            }

            if (favorite.Add(product))
                favoriteService.Update(favorite);

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);
        }
        public IActionResult Remove(Guid productId) 
        {
            Favorite? favorite = favoriteService.GetById(Info.Info.CommonFavoriteId);
            Product? product = productService.GetById(productId);

            if (favorite is null)
            {
                favorite = new Favorite() { Id = Info.Info.CommonFavoriteId };
                favoriteService.Add(favorite);
            }

            if (favorite.Remove(product))
                favoriteService.Update(favorite);

            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            Favorite? favorite = favoriteService.GetById(Info.Info.CommonFavoriteId);

            if (favorite is null)
            {
                favorite = new Favorite() { Id = Info.Info.CommonFavoriteId };
                favoriteService.Add(favorite);
            }

            favorite.Clear();
            favoriteService.Update(favorite);

            return RedirectToAction("Index");
        }
    }
}
