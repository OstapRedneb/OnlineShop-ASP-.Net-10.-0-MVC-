using System.Collections;

namespace OnlineShop.Models
{
    public record Comparator : IEnumerable<ProductViewModel>
    {
        private readonly List<ProductViewModel> _products;

        public Guid Id { get; set; }
        public int Count => _products.Count;

        public Guid UserId;


        //ctor
        public Comparator() : this(Guid.NewGuid())
        { }
        public Comparator(Guid id) : this(id, new List<ProductViewModel>())
        { }
        public Comparator(Guid id, List<ProductViewModel> products) 
        {
            Id = id;
            _products = products;
        }

        //IEnumerable
        public IEnumerator<ProductViewModel> GetEnumerator() => _products.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public ProductViewModel this[int index] 
        {
            get => _products[index];
            set => _products[index] = value;
        }


        //Methods
        public bool Add(ProductViewModel product) 
        {
            if (product is null || _products.Contains(product, new ProductIdEqualityComparer()))
                return false;

            _products.Add(product);
            return true;
        }
        public bool Remove(ProductViewModel product) 
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
