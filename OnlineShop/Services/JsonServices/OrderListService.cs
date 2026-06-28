using Newtonsoft.Json;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.JsonServices
{
    public class OrderListService : IOrderListService
    {
        private const string _path = "orderLists.json";


        public List<OrderList> GetAll()
        {
            string blob = GetOrderListsBlob();

            return JsonConvert
                    .DeserializeObject<List<OrderListData>>(blob)
                    ?.OfType<OrderListData>()
                    ?.Select(orderListData => (OrderList)orderListData)
                    ?.ToList() ?? new List<OrderList>();
        }
        public OrderList? GetById(Guid id)
        {
            List<OrderList> orderLists = GetAll();

            return orderLists.FirstOrDefault(orderList => orderList.Id == id);
        }
        public bool Add(OrderList orderList)
        {
            List<OrderList> orderLists = GetAll();

            if (orderList is null || orderLists.Any(orderListFromMemory => orderListFromMemory.Id == orderList.Id))
                return false;

            orderLists.Add(orderList);
            WriteIntoMemory(orderLists);

            return true;
        }
        public void AddRange(params List<OrderList> orderLists)
        {
            List<OrderList> orderListsFromMemory = GetAll();

            List<OrderList> newOrderLists = orderListsFromMemory.Union(orderLists, new OrderListIdEqualityComparer()).ToList();
            WriteIntoMemory(newOrderLists);
        }
        public bool Update(OrderList orderList)
        {
            List<OrderList> orderLists = GetAll();

            if (orderList is null)
                return false;

            bool wasFound = false;

            for (int i = 0; i < orderLists.Count; i++)
            {
                if (orderLists[i].Id == orderList.Id)
                {
                    orderLists[i] = orderList;
                    wasFound = true;
                    break;
                }
            }

            if (!wasFound)
                orderLists.Add(orderList);

            WriteIntoMemory(orderLists);
            return true;
        }
        public void Clear()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }
        private void WriteIntoMemory(List<OrderList> orderLists)
        {
            string blob = JsonConvert.SerializeObject(orderLists.OfType<OrderList>().Select(orderList => (OrderListData)orderList).ToList());

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.Write(blob);
            }
        }
        private string GetOrderListsBlob()
        {
            if (File.Exists(_path))
                using (StreamReader reader = new StreamReader(_path, false))
                    return reader.ReadToEnd();
            return "";
        }
    }
}
