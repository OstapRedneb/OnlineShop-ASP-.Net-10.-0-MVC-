using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IOrderListService
    {
        public List<OrderList> GetAll();
        public OrderList? GetById(Guid id);
        public bool Add(OrderList orderList);
        public void AddRange(params List<OrderList> orderLists);
        public bool Update(OrderList orderList);
        public void Clear();
    }
}
