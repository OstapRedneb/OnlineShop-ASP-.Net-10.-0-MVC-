namespace OnlineShop.Models
{
    public record OrderListData
    {
        public Guid Id { get; set; }
        public List<Order> Orders { get; set; }

        public OrderListData()
        { }
        public OrderListData(Guid id, List<Order> orders)
        {
            Id = id;
            Orders = orders;
        }

        public static explicit operator OrderList(OrderListData orderListData) 
        {
            return new OrderList(orderListData.Id, orderListData.Orders);
        }
        public static explicit operator OrderListData(OrderList orderList) 
        {
            return new OrderListData(orderList.Id, orderList.ToList());
        }
    }
}
