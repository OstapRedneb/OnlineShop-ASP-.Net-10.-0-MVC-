namespace OnlineShop.Models
{
    public record Position
    {
        public Guid Id { get; init; }
        public Product Product { get; init; }
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


        public Position() 
        { }
        public Position(Product product) : this(product, 1) 
        { }
        public Position(Product product, ushort quantity)
        {
            Id = Guid.NewGuid();
            Product = product;
            Quantity = quantity;
        }
    }
}
