using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ICartService, CartService>();
            builder.Services.AddTransient<IFavoriteService,  FavoriteService>();
            builder.Services.AddTransient<IOrderListService, OrderListService>();
            builder.Services.AddTransient<IComparatorService, ComparatorService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IRoleService, RoleService>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Initial}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
