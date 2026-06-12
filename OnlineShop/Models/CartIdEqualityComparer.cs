namespace OnlineShop.Models
{
    public class CartIdEqualityComparer : IEqualityComparer<Cart>
    {
        public bool Equals(Cart cart1, Cart cart2) => cart1?.Id == cart2?.Id;

        public int GetHashCode(Cart cart) => HashCode.Combine(cart.Id);
    }
}
