using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public static class CartService
    {
        private const string _path = "carts.json";


        public static List<Cart> GetAll()
        {
            string blob = GetCartsBlob();

            return JsonConvert
                    .DeserializeObject<List<CartData>>(blob)
                    ?.OfType<CartData>()
                    ?.Select(cartData => (Cart)cartData)
                    ?.ToList() ?? new List<Cart>();
        }
        public static Cart? GetById(Guid id) 
        {
            List<Cart> carts = GetAll();

            return carts.FirstOrDefault(cart => cart.Id == id);
        }
        public static bool Add(Cart cart) 
        {
            List<Cart> carts = GetAll();

            if (cart is null || carts.Any(cartFromMemory => cartFromMemory.Id == cart.Id))
                return false;

            carts.Add(cart);
            WriteIntoMemory(carts);

            return true;
        }
        public static void AddRange(params List<Cart> carts) 
        {
            List<Cart> cartsFromMemory = GetAll();

            List<Cart> newCarts = cartsFromMemory.Union(carts, new CartIdEqualityComparer()).ToList();
            WriteIntoMemory(newCarts);
        }
        public static bool Update(Cart cart) 
        {
            List<Cart> carts = GetAll();

            if (cart is null)
                return false;

            bool wasFound = false;

            for (int i = 0; i < carts.Count; i++) 
            {
                if (carts[i].Id == cart.Id) 
                {
                    carts[i] = cart;
                    wasFound = true;
                    break;
                }
            }

            if (!wasFound)
                carts.Add(cart);

            WriteIntoMemory(carts);
            return true;
        }
        public static void Clear()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }
        private static void WriteIntoMemory(List<Cart> carts)
        {
            string blob = JsonConvert.SerializeObject(carts.OfType<Cart>().Select(cart => (CartData)cart).ToList());

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.Write(blob);
            }
        }
        private static string GetCartsBlob()
        {
            if (File.Exists(_path))
                using (StreamReader reader = new StreamReader(_path, false))
                    return reader.ReadToEnd();
            return "";
        }
    }
}
