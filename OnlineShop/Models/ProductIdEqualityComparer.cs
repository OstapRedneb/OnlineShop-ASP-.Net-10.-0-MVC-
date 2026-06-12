using System;

namespace OnlineShop.Models;

public class ProductIdEqualityComparer : IEqualityComparer<Product>
{
    public bool Equals(Product product1, Product product2)
    {
        return product1?.Id == product2?.Id;
    }
    public int GetHashCode(Product product) => HashCode.Combine(product.Id);
}
