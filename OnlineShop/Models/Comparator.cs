using System.Collections;

namespace OnlineShop.Models
{
    public record Comparator : IEnumerable<Product>
    {
        private readonly List<Product> _products;

        public Guid Id { get; set; }
        public int Count => _products.Count;

        public Guid UserId;


        //ctor
        public Comparator() : this(Guid.NewGuid())
        { }
        public Comparator(Guid id) : this(id, new List<Product>())
        { }
        public Comparator(Guid id, List<Product> products) 
        {
            Id = id;
            _products = products;
        }

        //IEnumerable
        public IEnumerator<Product> GetEnumerator() => _products.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public Product this[int index] 
        {
            get => _products[index];
            set => _products[index] = value;
        }


        //Methods
        public bool Add(Product product) 
        {
            if (product is null || _products.Contains(product, new ProductIdEqualityComparer()))
                return false;

            _products.Add(product);
            return true;
        }
        public bool Remove(Product product) 
        {
            if (product is null || !_products.Contains(product, new ProductIdEqualityComparer()))
                return false;

            _products.Remove(product);
            return true;
        }
        public void Clear() 
        {
            _products.Clear();
        }
    }
}
