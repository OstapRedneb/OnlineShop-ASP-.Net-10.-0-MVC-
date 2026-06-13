using System;
using Newtonsoft.Json;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.JsonServices;

public class ProductService : IProductService
{
    private const string _path = "products.json";

    public List<Product> GetAll()
    {
        string blob = GetProductsBlob();

        return JsonConvert.DeserializeObject<List<Product>>(blob) ?? new List<Product>();
    }
    public Product? GetById(Guid id) 
    {
        return GetAll().FirstOrDefault(product => product.Id == id);
    }
    public bool Add(Product product)
    {
        bool answer = false;

        List<Product> products = GetAll();

        if (product != null && !products.Any(productFromMemory => productFromMemory.Id == product.Id))
        {
            products.Add(product);
            WriteIntoMemory(products);
            answer = true;
        }

        return answer;
    }
    public void AddRange(params List<Product> products)
    {
        List<Product> memoryProducts = GetAll();
        List<Product> productsToAdd = memoryProducts
            .Union(
                products.Where(product => product != null), 
                new ProductIdEqualityComparer()
            )
            .ToList();

        WriteIntoMemory(productsToAdd);
    }
    public void Clear()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }
    private void WriteIntoMemory(List<Product>  products)
    {
        string blob = JsonConvert.SerializeObject(products);

        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            writer.Write(blob);
        }
    }
    private string GetProductsBlob()
    {
        if (File.Exists(_path))
            using (StreamReader reader = new StreamReader(_path, false))
                return reader.ReadToEnd();
        return "";
    }
}
