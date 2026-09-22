namespace OnlineShop.Models
{
    public record ComparatorData(Guid Id, List<ProductViewModel> Products)
    {
        public ComparatorData() : this(Guid.NewGuid(), new List<ProductViewModel>())
        {}

        public static explicit operator Comparator(ComparatorData data) 
        {
            return new Comparator(data.Id, data.Products);
        }
        public static explicit operator ComparatorData(Comparator comparator) 
        {
            return new ComparatorData(comparator.Id, comparator.ToList());
        }
    }
}
