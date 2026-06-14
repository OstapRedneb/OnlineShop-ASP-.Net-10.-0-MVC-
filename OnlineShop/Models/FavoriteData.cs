namespace OnlineShop.Models
{
    public record FavoriteData(Guid Id, List<Product> Products)
    {
        public FavoriteData() : this(Guid.NewGuid(), new List<Product>())
        {}

        public static explicit operator Favorite(FavoriteData data) 
        {
            return new Favorite(data.Id, data.Products);
        }
        public static explicit operator FavoriteData(Favorite favorite) 
        {
            return new FavoriteData(favorite.Id, favorite.ToList());
        }
    }
}
