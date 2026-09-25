namespace OnlineShop.Models
{
    public record PositionViewModel
    {
        public Guid Id { get; init; }
        public ProductViewModel Product { get; init; }
        public ushort Quantity
        {
            get => field;
            set
            {
                if (value < 0)
                    field = 0; 
                else
                    field = value;
            }
        }
        public decimal Price => Product.Price * Quantity;


        public PositionViewModel() 
        { }
        public PositionViewModel(ProductViewModel product) : this(product, 1) 
        { }
        public PositionViewModel(ProductViewModel product, ushort quantity)
        {
            Id = Guid.NewGuid();
            Product = product;
            Quantity = quantity;
        }
    }
}
