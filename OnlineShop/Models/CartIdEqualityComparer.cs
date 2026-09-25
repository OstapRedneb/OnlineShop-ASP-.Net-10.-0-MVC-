namespace OnlineShop.Models
{
    public class CartIdEqualityComparer : IEqualityComparer<CartViewModel>
    {
        public bool Equals(CartViewModel cart1, CartViewModel cart2) => cart1?.Id == cart2?.Id;

        public int GetHashCode(CartViewModel cart) => HashCode.Combine(cart.Id);
    }
}
