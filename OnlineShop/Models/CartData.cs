namespace OnlineShop.Models
{
    public record CartData(Guid Id, List<Position> Positions)
    {
        public CartData() : this(Guid.NewGuid(), new List<Position>())
        { }

        public static explicit operator Cart(CartData cartData) 
        {
            return new Cart(cartData.Id, cartData.Positions);
        }
        public static explicit operator CartData(Cart cart) 
        {
            return new CartData(cart.Id, cart.ToList());
        }
    }
}
