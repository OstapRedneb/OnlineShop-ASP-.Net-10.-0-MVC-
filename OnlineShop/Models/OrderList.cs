using System.Collections;

namespace OnlineShop.Models
{
    public record OrderList : IEnumerable<Order>
    {
        private readonly List<Order> _orders = new List<Order>();
        public Guid Id { get; init; } = Guid.NewGuid();
        public int Count => _orders.Count;


        public OrderList(List<Order> orders) : this(Guid.NewGuid(), orders)
        {}
        public OrderList(Guid id, List<Order> orders)
        {
            Id = id;
            _orders = orders;
        }

        public Order this[int index]
        {
            get => _orders[index];
            set => _orders[index] = value;
        }

        public IEnumerator<Order> GetEnumerator() => _orders.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public bool Add(Order order) 
        {
            if (order is null || _orders.Contains(order, new OrderIdEqualityComparer()))
                return false;

            _orders.Add(order);
            return true;
        }
        public bool Remove(Order order) 
        {
            if (order is null || !_orders.Contains(order, new OrderIdEqualityComparer()))
                return false;

            _orders.Remove(order);
            return true;
        }
    }
}
