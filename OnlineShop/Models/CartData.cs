namespace OnlineShop.Models
{
    public record CartData(Guid Id, List<PositionViewModel> Positions)
    {
        public CartData() : this(Guid.NewGuid(), new List<PositionViewModel>())
        { }

        public static explicit operator CartViewModel(CartData cartData) 
        {
            return new CartViewModel(cartData.Id, cartData.Positions);
        }
        public static explicit operator CartData(CartViewModel cart) 
        {
            return new CartData(cart.Id, cart.ToList());
        }
    }
}
