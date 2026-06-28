namespace OnlineShop.Models
{
    public class OrderListIdEqualityComparer : IEqualityComparer<OrderList>
    {
        public bool Equals(OrderList orderList1, OrderList orderList2) 
        {
            return orderList1?.Id == orderList2?.Id;
        }
        public int GetHashCode(OrderList orderList) 
        {
            return HashCode.Combine(orderList.Id);
        }
    }
}
