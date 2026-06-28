namespace OnlineShop.Models
{
    public class OrderIdEqualityComparer : IEqualityComparer<Order>
    {
        public bool Equals(Order order1, Order order2) 
        {
            return order1?.Id == order2?.Id;
        }
        public int GetHashCode(Order order) 
        {
            return HashCode.Combine(order.Id);
        }
    }
}
