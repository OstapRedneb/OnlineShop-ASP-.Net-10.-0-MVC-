using OnlineShop.Db;
using OnlineShop.Db.Models;

namespace OnlineShop.Models
{
    public record PositionViewModel
    {
        public Guid Id { get; init; }
        public ProductViewModel Product { get; init; }
        public decimal Quantity
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
        public PositionViewModel(ProductViewModel product, decimal quantity)
        {
            Id = Guid.NewGuid();
            Product = product;
            Quantity = quantity;
        }

        //Mapping
        public static explicit operator Position(PositionViewModel positionViewModel)
        {
            return new Position()
            {
                Id = positionViewModel.Id, 
                Quantity = positionViewModel.Quantity,
                Product = new DatabaseContext()
                                .Products.FirstOrDefault(product => product.Id == positionViewModel.Product.Id)
                                ?? (Product)positionViewModel.Product
            };
        }
        public static explicit operator PositionViewModel(Position position)
        {
            return new PositionViewModel()
            {
                Id = position.Id,
                Quantity = position.Quantity,
                Product = (ProductViewModel)position.Product
            };
        }
    }
}
