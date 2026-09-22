using System;

namespace OnlineShop.Models;

public class ProductIdEqualityComparer : IEqualityComparer<ProductViewModel>
{
    public bool Equals(ProductViewModel product1, ProductViewModel product2)
    {
        return product1?.Id == product2?.Id;
    }
    public int GetHashCode(ProductViewModel product) => HashCode.Combine(product.Id);
}
